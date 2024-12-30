// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.IO;

namespace Pixeval.Extensions.Upscaler;

public static class IoHelper
{
    public static FileStream OpenAsyncRead(string path, int bufferSize = 4096)
    {
        return new(path, FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize, true);
    }

    public static FileStream OpenAsyncWrite(string path, int bufferSize = 4096)
    {
        return new(path, FileMode.Create, FileAccess.Write, FileShare.None, bufferSize, true);
    }

    public static FileStream OpenAsyncRead(this FileInfo info, int bufferSize = 4096)
    {
        return info.Open(new FileStreamOptions
        {
            Mode = FileMode.Open,
            Access = FileAccess.Read,
            Share = FileShare.Read,
            BufferSize = bufferSize,
            Options = FileOptions.Asynchronous
        });
    }

    public static FileStream OpenAsyncWrite(this FileInfo info, int bufferSize = 4096)
    {
        return info.Open(new FileStreamOptions
        {
            Mode = FileMode.OpenOrCreate,
            Access = FileAccess.Write,
            Share = FileShare.None,
            BufferSize = bufferSize,
            Options = FileOptions.Asynchronous
        });
    }
}
