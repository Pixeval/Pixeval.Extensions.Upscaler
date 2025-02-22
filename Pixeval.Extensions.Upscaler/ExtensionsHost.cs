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
public partial class ExtensionsHost : ExtensionsHostBase
{
    public static string TempDirectory { get; private set; } = "";

    public static string ExtensionDirectory { get; private set; } = "";

    public override string ExtensionName => "Real-ESRGAN 提升画质";

    public override string AuthorName => "Dylech30th";

    public override string ExtensionLink => "https://github.com/Pixeval/Pixeval.Extensions.Upscaler";

    public override string HelpLink => "https://github.com/dylech30th";

    public override string Description => "Pixeval AI 提升画质扩展";

    public override byte[]? Icon
    {
        get
        {
            var stream = typeof(ExtensionsHost).Assembly.GetManifestResourceStream("logo");
            if (stream is null)
                return null;
            var array = new byte[stream.Length];
            _ = stream.Read(array);
            return array;
        }
    }

    public override string Version => "1.0.0";

    public override IExtension[] Extensions { get; } =
    [
        new UpscalerModelSettingExtension(),
        new UpscalerOutputTypeSettingExtension(),
        new UpscaleImageTransformerExtension()
    ];

    public static ExtensionsHost Current { get; } = new();

    [UnmanagedCallersOnly(EntryPoint = nameof(DllGetExtensionsHost))]
    private static unsafe int DllGetExtensionsHost(void** ppv)
    {
        return DllGetExtensionsHost(ppv, Current);
    }

    public override void Initialize(string cultureName, string tempDirectory, string extensionDirectory)
    {
        TempDirectory = tempDirectory;
        ExtensionDirectory = extensionDirectory;
        CultureInfo.CurrentCulture = CultureInfo.CurrentUICulture = new(cultureName);
    }
}
