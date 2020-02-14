// Copyright (c) Six Labors and contributors.
// Licensed under the Apache License, Version 2.0.

using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing.Processors.Dithering;

namespace SixLabors.ImageSharp.Processing.Processors.Quantization
{
    /// <summary>
    /// Provides methods for allowing quantization of images pixels with configurable dithering.
    /// </summary>
    public interface IQuantizer
    {
        /// <summary>
        /// Gets the dithering algorithm to apply to the output image.
        /// </summary>
        IDither Dither { get; }

        /// <summary>
        /// Creates the generic frame quantizer
        /// </summary>
        /// <param name="configuration">The <see cref="Configuration"/> to configure internal operations.</param>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        /// <returns>The <see cref="IFrameQuantizer{TPixel}"/></returns>
        IFrameQuantizer<TPixel> CreateFrameQuantizer<TPixel>(Configuration configuration)
            where TPixel : struct, IPixel<TPixel>;

        /// <summary>
        /// Creates the generic frame quantizer
        /// </summary>
        /// <typeparam name="TPixel">The pixel format.</typeparam>
        /// <param name="configuration">The <see cref="Configuration"/> to configure internal operations.</param>
        /// <param name="maxColors">The maximum number of colors to hold in the color palette.</param>
        /// <returns>The <see cref="IFrameQuantizer{TPixel}"/></returns>
        IFrameQuantizer<TPixel> CreateFrameQuantizer<TPixel>(Configuration configuration, int maxColors)
            where TPixel : struct, IPixel<TPixel>;
    }
}
