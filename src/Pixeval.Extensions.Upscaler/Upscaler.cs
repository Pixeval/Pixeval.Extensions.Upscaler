// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;
using Pixeval.Extensions.Common;

namespace Pixeval.Extensions.Upscaler;

public class Upscaler : IDisposable, IAsyncDisposable
{
    public UpscalerModel Model { get; set; } = UpscalerModel.RealESRGANX4Plus;

    public int ScaleRatio { get; set; } = 4;

    public UpscalerOutputType OutputType { get; set; } = UpscalerOutputType.Png;

    public const string ProcessCompletedMark = "Completed";

    private static string ExecutablePath => @"C:\WorkSpace\Pixeval.Extensions.Upscaler\src\Pixeval.Extensions.Upscaler\Assets\RealESRGAN\realesrgan-ncnn-vulkan.exe";

    private Stream? _upscaleStream;

    private readonly SemaphoreSlim _runningSignal = new(1, 1);

    private bool _isDisposed;

    public async Task<Stream> UpscaleAsync(IStream stream)
    {
        ChannelWriter<string>? messageChannel = null;
        if (_isDisposed)
            throw new InvalidOperationException("This upscaler is already disposed");

        await _runningSignal.WaitAsync();
        try
        {
            var id = Guid.NewGuid().ToString();

            var tempFilePath = Path.Combine(ExtensionsHost.TempDirectory, id);
            stream.Seek(0, SeekOrigin.Begin, out _);

            // scoped-using is obligatory here, otherwise the file will be locked and the process will not be able to access it
            
            await using (var tempStream = IoHelper.OpenAsyncWrite(tempFilePath))
            {
                var buffer = new byte[4096];
                while (true)
                {
                    stream.Read(buffer, (uint)buffer.Length, out var bytesRead);
                    if (bytesRead is 0)
                        break;
                    await tempStream.WriteAsync(buffer);
                }
            }

            stream.Seek(0, SeekOrigin.Begin, out _);

            var modelParam = Model switch
            {
                UpscalerModel.RealESRGANX4Plus => "realesrgan-x4plus",
                UpscalerModel.RealESRNETX4Plus => "realesrnet-x4plus",
                UpscalerModel.RealESRGANX4PlusAnime => "realesrgan-x4plus-anime",
                _ => throw new ArgumentOutOfRangeException()
            };

            var outputType = OutputType switch
            {
                UpscalerOutputType.Png => "png",
                UpscalerOutputType.Jpeg => "jpg",
                UpscalerOutputType.WebP => "webp",
                _ => throw new ArgumentOutOfRangeException()
            };

            var outputFilePath = Path.Combine(ExtensionsHost.TempDirectory, $"{id}_out.{outputType}");

            var process = new Process();
            process.StartInfo.FileName = ExecutablePath;
            process.StartInfo.Arguments =
                $"-i {tempFilePath} -o {outputFilePath} -n {modelParam} -s {ScaleRatio}";
            process.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;

            _ = process.Start();

            _ = Task.Run(async () =>
            {
                while (!process.StandardError.EndOfStream && messageChannel is not null)
                {
                    await messageChannel.WriteAsync(await process.StandardError.ReadLineAsync() ?? "");
                }
            });

            await process.WaitForExitAsync();
            if (messageChannel is not null)
                await messageChannel.WriteAsync(ProcessCompletedMark);

            _upscaleStream = IoHelper.OpenAsyncRead(outputFilePath);
        }
        finally
        {
            _ = _runningSignal.Release();
        }

        return _upscaleStream;
    }

    public async void Dispose()
    {
        _isDisposed = true;
        await _runningSignal.WaitAsync();
        _runningSignal.Dispose();

        GC.SuppressFinalize(this);
        if (_upscaleStream != null)
            await _upscaleStream.DisposeAsync();
    }

    public async ValueTask DisposeAsync()
    {
        _isDisposed = true;
        await _runningSignal.WaitAsync();
        _runningSignal.Dispose();

        GC.SuppressFinalize(this);
        if (_upscaleStream != null)
            await _upscaleStream.DisposeAsync();
    }
}
