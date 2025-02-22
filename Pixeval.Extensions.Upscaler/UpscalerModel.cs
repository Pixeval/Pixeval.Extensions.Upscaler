// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.ComponentModel;

namespace Pixeval.Extensions.Upscaler;

public enum UpscalerModel
{
    [Description("realesrgan-x4plus")]
    RealESRGANX4Plus,

    [Description("realesrnet-x4plus")]
    RealESRNETX4Plus,

    [Description("realesrgan-x4plus-anime")]
    RealESRGANX4PlusAnime
}
