// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using FluentIcons.Common;
using Pixeval.Extensions.Common;
using Pixeval.Extensions.SDK.Transformers;
using Pixeval.Extensions.Upscaler.Strings;

namespace Pixeval.Extensions.Upscaler.Transformers;

[GeneratedComClass]
public partial class UpscaleImageTransformerExtension : ImageTransformerCommandExtensionBase
{
    public override void OnExtensionLoaded()
    {
        Upscaler = new();
    }

    public override void OnExtensionUnloaded()
    {
        Upscaler.Dispose();
    }

    public override async Task<IStream?> TransformAsync(IStream originalStream)
    {
        return (await Upscaler.UpscaleAsync(originalStream)).ToIStream();
    }

    public static Upscaler Upscaler { get; private set; } = null!;

    /// <inheritdoc />
    public override Symbol Icon => Symbol.ImageSparkle;

    /// <inheritdoc />
    public override string Label => Resource.UpscaleImageTransformerLabel;

    /// <inheritdoc />
    public override string Description => Label;
}
