// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.Marshalling;
using FluentIcons.Common;
using Pixeval.Extensions.SDK.Settings;

namespace Pixeval.Extensions.Upscaler.Settings;

[GeneratedComClass]
[Guid("1C03FD87-A5C8-4B45-95D7-6B11F0BBC000")]
public partial class UpscalerOutputTypeSettingExtension : EnumSettingsExtensionBase
{
    public override Symbol Icon => Symbol.ImageGlobe;

    public override string Token => nameof(UpscalerOutputType);

    public override string Label => "超分辨率生成图片的格式";

    public override string Description => "默认为PNG";

    public override void OnExtensionLoaded()
    {
    }

    public override void OnExtensionUnloaded()
    {
    }

    public override int DefaultValue => (int)UpscalerOutputType.Png;

    public override Type EnumType => typeof(UpscalerOutputType);
}
