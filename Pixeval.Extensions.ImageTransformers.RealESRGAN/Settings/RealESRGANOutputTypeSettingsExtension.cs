// Copyright (c) Pixeval.Extensions.RealESRGAN.
// Licensed under the GPL v3 License.

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.Marshalling;
using FluentIcons.Common;
using Pixeval.Extensions.ImageTransformers.RealESRGAN.Strings;
using Pixeval.Extensions.ImageTransformers.RealESRGAN.Transformers;
using Pixeval.Extensions.SDK.Settings;

namespace Pixeval.Extensions.ImageTransformers.RealESRGAN.Settings;

[GeneratedComClass]
public partial class RealESRGANOutputTypeSettingsExtension : EnumSettingsExtensionBase
{
    public override Symbol Icon => Symbol.ImageGlobe;

    public override string Token => nameof(RealESRGANOutputType);

    public override string Label => Resource.RealESRGANOutputTypeSettingsLabel;

    public override string Description => Resource.RealESRGANOutputTypeSettingsDescription;

    public override void OnValueChanged(int value)
    {
        SuperResolutionImageTransformerExtension.RealESRGAN.OutputType = (RealESRGANOutputType) value;
    }

    public override int DefaultValue => (int) RealESRGANOutputType.PNG;

    /// <inheritdoc />
    public override IReadOnlyDictionary<string, int> EnumKeyValues => new Dictionary<string, int>
    {
        [nameof(RealESRGANOutputType.PNG)] = (int) RealESRGANOutputType.PNG,
        [nameof(RealESRGANOutputType.JPG)] = (int) RealESRGANOutputType.JPG,
        [nameof(RealESRGANOutputType.WebP)] = (int) RealESRGANOutputType.WebP
    };
}
