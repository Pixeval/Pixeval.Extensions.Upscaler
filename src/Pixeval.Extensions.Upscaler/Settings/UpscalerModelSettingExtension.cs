// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System;
using System.Runtime.InteropServices.Marshalling;
using FluentIcons.Common;
using Pixeval.Extensions.SDK.Settings;

namespace Pixeval.Extensions.Upscaler.Settings;

[GeneratedComClass]
public partial class UpscalerModelSettingExtension : EnumSettingsExtensionBase
{
    public override Symbol Icon => Symbol.EyeTracking;

    public override string Token => nameof(UpscalerModel);

    public override string Label => "AI超分辨率模型";

    public override string Description => "选择AI超分辨率模型，详见此处";

    public override string DescriptionUri => "https://github.com/xinntao/Real-ESRGAN/blob/master/README_CN.md";

    public override void OnValueChanged(int value)
    {
        ExtensionsHost.Upscaler.Model = (UpscalerModel)value;
    }

    public override void OnExtensionLoaded()
    {
    }

    public override void OnExtensionUnloaded()
    {
    }

    public override int DefaultValue => (int)UpscalerModel.RealESRGANX4Plus;

    public override string[]? EnumStrings => null;

    public override Type EnumType => typeof(UpscalerModel);
}
