// Copyright (c) Six Labors.
// Licensed under the Six Labors Split License.

using System.Numerics;
using System.Runtime.CompilerServices;
using SixLabors.ImageSharp.PixelFormats;

// ReSharper disable InconsistentNaming
namespace SixLabors.ImageSharp.Tests.PixelFormats;

[Trait("Category", "PixelFormats")]
public class L8Tests
{
    public static readonly TheoryData<byte> LuminanceData
        = new() { 0, 1, 2, 3, 5, 13, 31, 71, 73, 79, 83, 109, 127, 128, 131, 199, 250, 251, 254, 255 };

    [Theory]
    [InlineData(0)]
    [InlineData(255)]
    [InlineData(10)]
    [InlineData(42)]
    public void L8_PackedValue_EqualsInput(byte input)
        => Assert.Equal(input, new L8(input).PackedValue);

    [Fact]
    public void AreEqual()
    {
        L8 color1 = new(100);
        L8 color2 = new(100);

        Assert.Equal(color1, color2);
    }

    [Fact]
    public void AreNotEqual()
    {
        L8 color1 = new(100);
        L8 color2 = new(200);

        Assert.NotEqual(color1, color2);
    }

    [Fact]
    public void L8_FromScaledVector4()
    {
        // Arrange
        L8 gray = default;
        const byte expected = 128;
        Vector4 scaled = new L8(expected).ToScaledVector4();

        // Act
        gray.FromScaledVector4(scaled);
        byte actual = gray.PackedValue;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(LuminanceData))]
    public void L8_ToScaledVector4(byte input)
    {
        // Arrange
        L8 gray = new(input);

        // Act
        Vector4 actual = gray.ToScaledVector4();

        // Assert
        float scaledInput = input / 255F;
        Assert.Equal(scaledInput, actual.X);
        Assert.Equal(scaledInput, actual.Y);
        Assert.Equal(scaledInput, actual.Z);
        Assert.Equal(1, actual.W);
    }

    [Theory]
    [MemberData(nameof(LuminanceData))]
    public void L8_FromVector4(byte luminance)
    {
        // Arrange
        L8 gray = default;
        Vector4 vector = new L8(luminance).ToVector4();

        // Act
        gray.FromVector4(vector);
        byte actual = gray.PackedValue;

        // Assert
        Assert.Equal(luminance, actual);
    }

    [Theory]
    [MemberData(nameof(LuminanceData))]
    public void L8_ToVector4(byte input)
    {
        // Arrange
        L8 gray = new(input);

        // Act
        Vector4 actual = gray.ToVector4();

        // Assert
        float scaledInput = input / 255F;
        Assert.Equal(scaledInput, actual.X);
        Assert.Equal(scaledInput, actual.Y);
        Assert.Equal(scaledInput, actual.Z);
        Assert.Equal(1, actual.W);
    }

    [Theory]
    [MemberData(nameof(LuminanceData))]
    public void L8_FromRgba32(byte rgb)
    {
        // Arrange
        L8 gray = default;
        byte expected = ColorNumerics.Get8BitBT709Luminance(rgb, rgb, rgb);

        // Act
        gray.FromRgba32(new Rgba32(rgb, rgb, rgb));
        byte actual = gray.PackedValue;

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(LuminanceData))]
    public void L8_ToRgba32(byte luminance)
    {
        // Arrange
        L8 gray = new(luminance);

        // Act
        Rgba32 actual = default;
        gray.ToRgba32(ref actual);

        // Assert
        Assert.Equal(luminance, actual.R);
        Assert.Equal(luminance, actual.G);
        Assert.Equal(luminance, actual.B);
        Assert.Equal(byte.MaxValue, actual.A);
    }

    [Fact]
    public void L8_FromBgra5551()
    {
        // arrange
        L8 grey = default;
        byte expected = byte.MaxValue;

        // act
        grey.FromBgra5551(new Bgra5551(1.0f, 1.0f, 1.0f, 1.0f));

        // assert
        Assert.Equal(expected, grey.PackedValue);
    }

    public class Rgba32Compatibility
    {
        // ReSharper disable once MemberHidesStaticFromOuterClass
        public static readonly TheoryData<byte> LuminanceData = L8Tests.LuminanceData;

        [Theory]
        [MemberData(nameof(LuminanceData))]
        public void L8_FromRgba32_IsInverseOf_ToRgba32(byte luminance)
        {
            L8 original = new(luminance);

            Rgba32 rgba = default;
            original.ToRgba32(ref rgba);

            L8 mirror = default;
            mirror.FromRgba32(rgba);

            Assert.Equal(original, mirror);
        }

        [Theory]
        [MemberData(nameof(LuminanceData))]
        public void Rgba32_ToL8_IsInverseOf_L8_ToRgba32(byte luminance)
        {
            L8 original = new(luminance);

            Rgba32 rgba = default;
            original.ToRgba32(ref rgba);

            L8 mirror = default;
            mirror.FromRgba32(rgba);

            Assert.Equal(original, mirror);
        }

        [Theory]
        [MemberData(nameof(LuminanceData))]
        public void ToVector4_IsRgba32Compatible(byte luminance)
        {
            L8 original = new(luminance);

            Rgba32 rgba = default;
            original.ToRgba32(ref rgba);

            Vector4 l8Vector = original.ToVector4();
            Vector4 rgbaVector = original.ToVector4();

            Assert.Equal(l8Vector, rgbaVector, new ApproximateFloatComparer(1e-5f));
        }

        [Theory]
        [MemberData(nameof(LuminanceData))]
        public void FromVector4_IsRgba32Compatible(byte luminance)
        {
            L8 original = new(luminance);

            Rgba32 rgba = default;
            original.ToRgba32(ref rgba);

            Vector4 rgbaVector = original.ToVector4();

            L8 mirror = default;
            mirror.FromVector4(rgbaVector);

            Assert.Equal(original, mirror);
        }

        [Theory]
        [MemberData(nameof(LuminanceData))]
        public void ToScaledVector4_IsRgba32Compatible(byte luminance)
        {
            L8 original = new(luminance);

            Rgba32 rgba = default;
            original.ToRgba32(ref rgba);

            Vector4 l8Vector = original.ToScaledVector4();
            Vector4 rgbaVector = original.ToScaledVector4();

            Assert.Equal(l8Vector, rgbaVector, new ApproximateFloatComparer(1e-5f));
        }

        [Theory]
        [MemberData(nameof(LuminanceData))]
        public void FromScaledVector4_IsRgba32Compatible(byte luminance)
        {
            L8 original = new(luminance);

            Rgba32 rgba = default;
            original.ToRgba32(ref rgba);

            Vector4 rgbaVector = original.ToScaledVector4();

            L8 mirror = default;
            mirror.FromScaledVector4(rgbaVector);

            Assert.Equal(original, mirror);
        }

        [Fact]
        public void L8_PixelInformation()
        {
            PixelTypeInfo info = L8.GetPixelTypeInfo();
            Assert.Equal(Unsafe.SizeOf<L8>() * 8, info.BitsPerPixel);
            Assert.Equal(PixelAlphaRepresentation.None, info.AlphaRepresentation);
            Assert.Equal(PixelColorType.Grayscale, info.ColorType);

            PixelComponentInfo componentInfo = info.ComponentInfo.Value;
            Assert.Equal(1, componentInfo.ComponentCount);
            Assert.Equal(0, componentInfo.Padding);
            Assert.Equal(8, componentInfo.GetComponentPrecision(0));
            Assert.Equal(8, componentInfo.GetMaximumComponentPrecision());
        }
    }
}
