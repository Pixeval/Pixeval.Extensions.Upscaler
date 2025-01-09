// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using FluentIcons.Common;
using Pixeval.Extensions.Common;
using Pixeval.Extensions.SDK.Transformers;

namespace Pixeval.Extensions.Upscaler.Transformers;

[GeneratedComClass]
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

    /// <inheritdoc />
    public override Symbol Icon => Symbol.ImageSparkle;

    /// <inheritdoc />
    public override string Label => "提升画质";

    /// <inheritdoc />
    public override string Description => Label;
}
