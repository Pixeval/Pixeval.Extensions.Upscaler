// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.Globalization;
using Pixeval.Extensions.Common;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using Pixeval.Extensions.SDK;
using Pixeval.Extensions.Upscaler.Settings;
using Pixeval.Extensions.Upscaler.Transformers;

namespace Pixeval.Extensions.Upscaler;

[GeneratedComClass]
[Guid("15946DBC-7928-4CF9-B63D-D5340756235E")]
public partial class ExtensionsHost : ExtensionsHostBase
{
    public static string TempDirectory { get; private set; } = "";

    public static CultureInfo Culture { get; private set; } = null!;

    public override string ExtensionName => "Pixeval Upscaler Extension";

    public override string AuthorName => "Pixeval";

    public override string ExtensionLink => "https://github.com/Pixeval/Pixeval.Extensions.Upscaler";

    public override string HelpLink => "https://github.com/Pixeval/Pixeval.Extensions.Upscaler";

    public override string Description => "Pixeval Upscaler Extension";

    public override string Version => "1.0.0";

    public override IExtension[] Extensions { get; } =
    [
        new UpscalerModelSettingExtension(),
        new UpscalerScaleRatioSettingExtension(),
        new UpscalerOutputTypeSettingExtension(),
        new UpscaleImageTransformerExtension()
    ];

    public static ExtensionsHost Current { get; } = new();

    [UnmanagedCallersOnly(EntryPoint = nameof(DllGetExtensionsHost))]
    private static unsafe int DllGetExtensionsHost(void** ppv)
    {
        return DllGetExtensionsHost(ppv, Current);
    }

    public static Upscaler Upscaler { get; } = new();

    public override void Initialize(string cultureBcl47, string tempDirectory)
    {
        TempDirectory = tempDirectory;
        Culture = new(cultureBcl47);
    }
}
