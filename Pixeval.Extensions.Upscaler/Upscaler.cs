// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Pixeval.Extensions.Common;

namespace Pixeval.Extensions.Upscaler;

public class Upscaler : IDisposable, IAsyncDisposable
{
    public UpscalerModel Model { get; set; }

    public UpscalerOutputType OutputType { get; set; }

    private static string ExecutablePath => Path.Combine(ExtensionsHost.ExtensionDirectory, @"Pixeval.Extensions.Upscaler.Assets\RealESRGAN\realesrgan-ncnn-vulkan.exe");

    private readonly SemaphoreSlim _runningSignal = new(1, 1);

    private bool _isDisposed;

    public async Task<Stream> UpscaleAsync(IStream stream)
    {
        if (_isDisposed)
            throw new InvalidOperationException("This upscaler is already disposed");

        await _runningSignal.WaitAsync();
        try
        {
            var id = Guid.NewGuid().ToString();

            var tempFilePath = Path.Combine(ExtensionsHost.TempDirectory, id);
            stream.Seek(0, SeekOrigin.Begin);

            // scoped-using is obligatory here, otherwise the file will be locked and the process will not be able to access it
            
            await using (var tempStream = Helper.OpenAsyncWrite(tempFilePath)) 
                await stream.CopyToAsync(tempStream.ToIStream());

            stream.Seek(0, SeekOrigin.Begin);

            var modelParam = Model.GetDescription();
            var outputType = OutputType.GetDescription();
            var outputFilePath = Path.Combine(ExtensionsHost.TempDirectory, $"{id}_out.{outputType}");

            using var process = new Process();
            process.StartInfo = new()
            {
                FileName = ExecutablePath,
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                Arguments = $"-i \"{tempFilePath}\" -o \"{outputFilePath}\" -n {modelParam}"
            };

            _ = process.Start();

            await process.WaitForExitAsync();

            return Helper.OpenAsyncRead(outputFilePath);
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
