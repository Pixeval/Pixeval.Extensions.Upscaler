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
public partial class RealESRGANModelSettingsExtension : EnumSettingsExtensionBase
{
    public override Symbol Icon => Symbol.EyeTracking;

    public override string Token => nameof(RealESRGANModel);

    public override string Label => Resource.RealESRGANModelSettingsLabel;

    public override string Description => Resource.RealESRGANModelSettingsDescription;

    public override string DescriptionUri => "https://github.com/xinntao/Real-ESRGAN/blob/master/README_CN.md";

    public override void OnValueChanged(int value)
    {
        SuperResolutionImageTransformerExtension.RealESRGAN.Model = (RealESRGANModel)value;
    }

    public override int DefaultValue => (int)RealESRGANModel.RealESRGANX4Plus;

    /// <inheritdoc />
    public override IReadOnlyDictionary<string, int> EnumKeyValues => new Dictionary<string, int>
    {
        [nameof(RealESRGANModel.RealESRGANX4Plus)] = (int) RealESRGANModel.RealESRGANX4Plus,
        [nameof(RealESRGANModel.RealESRGANX4PlusAnime)] = (int) RealESRGANModel.RealESRGANX4PlusAnime
    };
}
