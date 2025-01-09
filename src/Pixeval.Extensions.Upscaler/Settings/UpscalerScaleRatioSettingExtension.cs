// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.Runtime.InteropServices.Marshalling;
using FluentIcons.Common;
using Pixeval.Extensions.SDK.Settings;

namespace Pixeval.Extensions.Upscaler.Settings;

[GeneratedComClass]
public partial class UpscalerScaleRatioSettingExtension : IntSettingsExtensionBase
{
    public override Symbol Icon => Symbol.RatioOneToOne;

    public override string Token => "UpscalerScaleRatio";

    public override string Label => "AI超分辨率倍率";

    public override string Description => "默认为4，注意过大的倍率可能导致生成的图片体积过大影响性能";

    public override void OnValueChanged(int value)
    {
        ExtensionsHost.Upscaler.ScaleRatio = value;
    }

    public override void OnExtensionLoaded()
    {
    }

    public override void OnExtensionUnloaded()
    {
    }

    public override int DefaultValue => 4;

    public override int MinValue => 2;

    public override int MaxValue => 4;

    public override string? Placeholder => null;
}
