// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System;
using System.IO;
using System.Reflection;

namespace Pixeval.Extensions.Upscaler;

public static class Helper
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

    public static string GetDescription<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] TEnum>(this TEnum @enum) where TEnum : Enum
    {
        return (typeof(TEnum).GetField(@enum.ToString())?.GetCustomAttribute(typeof(DescriptionAttribute)) as DescriptionAttribute)?.Description ?? throw new Exception("Attribute not found");
    }
}
