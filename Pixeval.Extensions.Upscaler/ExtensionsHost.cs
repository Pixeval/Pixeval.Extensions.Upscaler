// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using Pixeval.Extensions.Common;
using System.Runtime.InteropServices.Marshalling;
using System.Runtime.InteropServices;
using Pixeval.Extensions.SDK;
using Pixeval.Extensions.Upscaler.Settings;
using Pixeval.Extensions.Upscaler.Strings;
using Pixeval.Extensions.Upscaler.Transformers;

namespace Pixeval.Extensions.Upscaler;

[GeneratedComClass]
public partial class ExtensionsHost : ExtensionsHostBase
{
    public override string ExtensionName => Resource.ExtensionHostName;

    public override string AuthorName => "Dylech30th";

    public override string ExtensionLink => "https://github.com/Pixeval/Pixeval.Extensions.Upscaler";

    public override string HelpLink => "https://github.com/dylech30th";

    public override string Description => Resource.ExtensionHostDescription;

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
        new UpscalerModelSettingsExtension(),
        new UpscalerOutputTypeSettingsExtension(),
        new UpscaleImageTransformerExtension()
    ];

    public static ExtensionsHost Current { get; } = new();

    [UnmanagedCallersOnly(EntryPoint = nameof(DllGetExtensionsHost))]
    private static unsafe int DllGetExtensionsHost(void** ppv) => DllGetExtensionsHost(ppv, Current);
}
