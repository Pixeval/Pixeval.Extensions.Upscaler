// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System;
using System.Runtime.InteropServices.Marshalling;
using FluentIcons.Common;
using Pixeval.Extensions.SDK.Settings;
using Pixeval.Extensions.Upscaler.Strings;
using Pixeval.Extensions.Upscaler.Transformers;

namespace Pixeval.Extensions.Upscaler.Settings;

[GeneratedComClass]
public partial class UpscalerModelSettingsExtension : EnumSettingsExtensionBase
{
    public override Symbol Icon => Symbol.EyeTracking;

    public override string Token => nameof(UpscalerModel);

    public override string Label => Resource.UpscalerModelSettingsLabel;

    public override string Description => Resource.UpscalerModelSettingsDescription;

    public override string DescriptionUri => "https://github.com/xinntao/Real-ESRGAN/blob/master/README_CN.md";

    public override void OnValueChanged(int value)
    {
        UpscaleImageTransformerExtension.Upscaler.Model = (UpscalerModel)value;
    }

    public override int DefaultValue => (int)UpscalerModel.RealESRGANX4Plus;

    public override string[]? EnumStrings => null;

    public override Type EnumType => typeof(UpscalerModel);
}
