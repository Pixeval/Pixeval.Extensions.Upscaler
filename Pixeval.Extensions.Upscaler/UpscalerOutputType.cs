// Copyright (c) Pixeval.Extensions.Upscaler.
// Licensed under the GPL v3 License.

using System.ComponentModel;

namespace Pixeval.Extensions.Upscaler;

public enum UpscalerOutputType
{
    [Description("png")]
    Png,

    [Description("jpg")]
    Jpg,

    [Description("webp")]
    WebP
}
