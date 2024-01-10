// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using SixLabors.ImageSharp.PixelFormats;

namespace SixLabors.ImageSharp.Formats.Tiff;

/// <summary>
/// Image decoder for generating an image out of a TIFF stream.
/// </summary>
public class TiffDecoder : SpecializedImageDecoder<TiffDecoderOptions>
{
    private TiffDecoder()
    {
    }

    /// <summary>
    /// Gets the shared instance.
    /// </summary>
    public static TiffDecoder Instance { get; } = new();

    /// <inheritdoc/>
    protected override ImageInfo Identify(DecoderOptions options, Stream stream, CancellationToken cancellationToken)
    {
        Guard.NotNull(options, nameof(options));
        Guard.NotNull(stream, nameof(stream));

        TiffDecoderOptions tiffOptions = new() { GeneralOptions = options };
        return new TiffDecoderCore(tiffOptions).Identify(options.Configuration, stream, cancellationToken);
    }

    /// <inheritdoc/>
    protected override Image<TPixel> Decode<TPixel>(TiffDecoderOptions options, Stream stream, CancellationToken cancellationToken)
    {
        Guard.NotNull(options, nameof(options));
        Guard.NotNull(stream, nameof(stream));

        TiffDecoderCore decoder = new(options);
        Image<TPixel> image = decoder.Decode<TPixel>(options.GeneralOptions.Configuration, stream, cancellationToken);

        ScaleToTargetSize(options.GeneralOptions, image);

        return image;
    }

    /// <inheritdoc/>
    protected override Image Decode(TiffDecoderOptions options, Stream stream, CancellationToken cancellationToken)
        => this.Decode<Rgba32>(options, stream, cancellationToken);

    /// <inheritdoc/>
    protected override TiffDecoderOptions CreateDefaultSpecializedOptions(DecoderOptions options)
        => new() { GeneralOptions = options };
}
