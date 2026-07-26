// Copyright (c) Pixeval.Extensions.RealESRGAN.
// Licensed under the GPL v3 License.

using System.ComponentModel;

namespace Pixeval.Extensions.ImageTransformers.RealESRGAN;

public enum RealESRGANOutputType
{
    [Description("png")]
    PNG,

    [Description("jpg")]
    JPG,

    [Description("webp")]
    WebP
}
