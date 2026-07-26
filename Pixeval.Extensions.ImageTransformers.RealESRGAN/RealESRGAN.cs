// Copyright (c) Pixeval.Extensions.RealESRGAN.
// Licensed under the GPL v3 License.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Pixeval.Extensions.SDK;

namespace Pixeval.Extensions.ImageTransformers.RealESRGAN;

public class RealESRGAN : IDisposable, IAsyncDisposable
{
    public RealESRGANModel Model { get; set; }

    public RealESRGANOutputType OutputType { get; set; }

    private static string RealESRGANDirectory => Path.Combine(ExtensionsHostBase.ExtensionDirectory, "RealESRGAN");

    private static string ExecutablePath => Path.Combine(
        RealESRGANDirectory,
        OperatingSystem.IsWindows() ? "realesrgan-ncnn-vulkan.exe" : "realesrgan-ncnn-vulkan");

    private static string ModelsDirectory => Path.Combine(RealESRGANDirectory, "models");

    private readonly SemaphoreSlim _runningSignal = new(1, 1);

    private bool _isDisposed;

    public async Task SuperResolveAsync(Stream originalStream, Stream destinationStream)
    {
        if (_isDisposed)
            throw new InvalidOperationException("This Real-ESRGAN instance is already disposed");

        await _runningSignal.WaitAsync();
        try
        {
            var id = Guid.NewGuid().ToString();

            var tempFilePath = Path.Combine(ExtensionsHostBase.TempDirectory, id);
            _ = originalStream.Seek(0, SeekOrigin.Begin);

            // scoped-using is obligatory here, otherwise the file will be locked and the process will not be able to access it
            
            await using (var tempStream = Helper.OpenAsyncWrite(tempFilePath)) 
                await originalStream.CopyToAsync(tempStream);

            _ = originalStream.Seek(0, SeekOrigin.Begin);

            var modelParam = Model.GetDescription();
            var outputType = OutputType.GetDescription();
            var outputFilePath = Path.Combine(ExtensionsHostBase.TempDirectory, $"{id}_out.{outputType}");

            using var process = new Process();
            process.StartInfo = new()
            {
                FileName = ExecutablePath,
                WorkingDirectory = RealESRGANDirectory,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                ArgumentList =
                {
                    "-i", tempFilePath,
                    "-o", outputFilePath,
                    "-m", ModelsDirectory,
                    "-n", modelParam
                }
            };

            _ = process.Start();

            await process.WaitForExitAsync();

            await using var outputStream = Helper.OpenAsyncRead(outputFilePath);
            await outputStream.CopyToAsync(destinationStream);
        }
        finally
        {
            _ = _runningSignal.Release();
        }
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
        if (_isDisposed)
            return;
        _isDisposed = true;
        _runningSignal.Wait();
        _runningSignal.Dispose();
    }

    public async ValueTask DisposeAsync()
    {
        _isDisposed = true;
        await _runningSignal.WaitAsync();
        _runningSignal.Dispose();

        GC.SuppressFinalize(this);
    }
}
