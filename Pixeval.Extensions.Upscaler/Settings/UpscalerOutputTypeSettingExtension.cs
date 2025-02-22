// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System;
using System.Runtime.InteropServices.Marshalling;
using FluentIcons.Common;
using Pixeval.Extensions.SDK.Settings;
using Pixeval.Extensions.Upscaler.Transformers;

namespace Pixeval.Extensions.Upscaler.Settings;

[GeneratedComClass]
public partial class UpscalerOutputTypeSettingExtension : EnumSettingsExtensionBase
{
    public override Symbol Icon => Symbol.ImageGlobe;

    public override string Token => nameof(UpscalerOutputType);

    public override string Label => "超分辨率生成图片的格式";

    public override string Description => "默认为PNG";

    public override void OnValueChanged(int value)
    {
        UpscaleImageTransformerExtension.Upscaler.OutputType = (UpscalerOutputType)value;
    }

    public override void OnExtensionLoaded()
    {
    }

    public override void OnExtensionUnloaded()
    {
    }

    public override int DefaultValue => (int)UpscalerOutputType.Png;

    public override string[]? EnumStrings => null;

    public override Type EnumType => typeof(UpscalerOutputType);
}
