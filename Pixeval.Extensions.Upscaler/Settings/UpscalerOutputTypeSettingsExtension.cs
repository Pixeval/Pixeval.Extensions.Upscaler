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
public partial class UpscalerOutputTypeSettingsExtension : EnumSettingsExtensionBase
{
    public override Symbol Icon => Symbol.ImageGlobe;

    public override string Token => nameof(UpscalerOutputType);

    public override string Label => Resource.UpscalerOutputTypeSettingsLabel;

    public override string Description => Resource.UpscalerOutputTypeSettingsDescription;

    public override void OnValueChanged(int value)
    {
        UpscaleImageTransformerExtension.Upscaler.OutputType = (UpscalerOutputType)value;
    }

    public override int DefaultValue => (int)UpscalerOutputType.Png;

    public override string[]? EnumStrings => null;

    public override Type EnumType => typeof(UpscalerOutputType);
}
