// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

namespace SixLabors.ImageSharp.Formats.Tiff;

/// <summary>
/// Configuration options for decoding TIFF images.
/// </summary>
public sealed class TiffDecoderOptions : ISpecializedDecoderOptions
{
    /// <inheritdoc/>
    public DecoderOptions GeneralOptions { get; init; } = new();

    /// <summary>
    /// Gets the index of the frame to decode from. Inclusive.
    /// </summary>
    public uint FrameIndex { get; init; }
}
