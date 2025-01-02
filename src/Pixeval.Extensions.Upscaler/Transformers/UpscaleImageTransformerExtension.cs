// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using Pixeval.Extensions.Common;
using Pixeval.Extensions.SDK.Transformers;

namespace Pixeval.Extensions.Upscaler.Transformers;

[GeneratedComClass]
[Guid("0628396E-7A74-4EBA-AD7F-C76892FB8041")]
public partial class UpscaleImageTransformerExtension : ImageTransformerExtensionBase
{
    public override void OnExtensionLoaded()
    {
    }

    public override void OnExtensionUnloaded()
    {
    }

    public override async Task<IStream?> TransformAsync(IStream originalStream)
    {
        return (await ExtensionsHost.Upscaler.UpscaleAsync(originalStream)).ToIStream();
    }
}
