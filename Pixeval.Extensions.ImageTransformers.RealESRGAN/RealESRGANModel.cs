// Copyright (c) Pixeval.Extensions.RealESRGAN.
// Licensed under the GPL v3 License.

using System.ComponentModel;

namespace Pixeval.Extensions.ImageTransformers.RealESRGAN;

public enum RealESRGANModel
{
    [Description("realesrgan-x4plus")]
    RealESRGANX4Plus = 0,

    [Description("realesrgan-x4plus-anime")]
    RealESRGANX4PlusAnime = 2
}
