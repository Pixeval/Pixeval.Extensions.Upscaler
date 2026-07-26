// Copyright (c) Pixeval.Extensions.RealESRGAN.
// Licensed under the GPL v3 License.

using System.IO;
using System.Runtime.InteropServices.Marshalling;
using System.Threading.Tasks;
using FluentIcons.Common;
using Pixeval.Extensions.ImageTransformers.RealESRGAN.Strings;
using Pixeval.Extensions.SDK.Transformers;

namespace Pixeval.Extensions.ImageTransformers.RealESRGAN.Transformers;

[GeneratedComClass]
public partial class SuperResolutionImageTransformerExtension : ImageTransformerCommandExtensionBase
{
    public override void OnExtensionLoaded()
    {
        RealESRGAN = new();
    }

    public override void OnExtensionUnloaded()
    {
        RealESRGAN.Dispose();
    }

    public override Task TransformAsync(Stream originalStream, Stream destinationStream) =>
        RealESRGAN.SuperResolveAsync(originalStream, destinationStream);

    public static RealESRGAN RealESRGAN { get; private set; } = null!;

    /// <inheritdoc />
    public override Symbol Icon => Symbol.ImageSparkle;

    /// <inheritdoc />
    public override string Label => Resource.SuperResolutionImageTransformerLabel;

    /// <inheritdoc />
    public override string Description => Label;
}
