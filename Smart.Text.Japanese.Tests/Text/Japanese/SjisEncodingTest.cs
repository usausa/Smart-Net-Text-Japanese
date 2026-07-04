namespace Smart.Text.Japanese;

public sealed class SjisEncodingTest
{
    // Calc

    [Fact]
    public void IsSingleByte()
    {
        // ASCII is single byte
        Assert.True(SjisEncoding.IsSingleByte('A'));

        // Half-width kana (U+FF61 - U+FF9F) is single byte in Shift-JIS
        Assert.True(SjisEncoding.IsSingleByte('｡'));
        Assert.True(SjisEncoding.IsSingleByte('ｱ'));
        Assert.True(SjisEncoding.IsSingleByte('ﾟ'));

        // Just outside the half-width kana range is not single byte
        Assert.False(SjisEncoding.IsSingleByte('｠'));
        Assert.False(SjisEncoding.IsSingleByte('ﾠ'));

        // Full-width is not single byte
        Assert.False(SjisEncoding.IsSingleByte('あ'));

        // The predicate matches the actual Shift-JIS byte count
        Assert.Equal(1, SjisEncoding.GetByteCount("ｱ"));
        Assert.Equal(2, SjisEncoding.GetByteCount("あ"));
        Assert.Equal(2, SjisEncoding.GetByteCount("ｶﾞ"));
    }

    // Fixed bytes

    [Fact]
    public void FixedAlignmentLeft()
    {
        // Left align keeps the content at the head and pads on the right
        Assert.Equal(SjisEncoding.GetBytes("あ "), SjisEncoding.GetFixedBytes("あ", 3, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("あA "), SjisEncoding.GetFixedBytes("あA", 4, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("Aあ "), SjisEncoding.GetFixedBytes("Aあ", 4, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A "), SjisEncoding.GetFixedBytes("Aあ", 2, FixedAlignment.Left));   // 2-byte あ does not fit in the remaining 1 byte
    }

    [Fact]
    public void FixedAlignmentRight()
    {
        // Right align keeps the content at the tail and pads on the left
        Assert.Equal(SjisEncoding.GetBytes(" あ"), SjisEncoding.GetFixedBytes("あ", 3, FixedAlignment.Right));
        Assert.Equal(SjisEncoding.GetBytes(" あA"), SjisEncoding.GetFixedBytes("あA", 4, FixedAlignment.Right));
        Assert.Equal(SjisEncoding.GetBytes(" Aあ"), SjisEncoding.GetFixedBytes("Aあ", 4, FixedAlignment.Right));
        Assert.Equal(SjisEncoding.GetBytes("あ"), SjisEncoding.GetFixedBytes("あ", 2, FixedAlignment.Right));   // exact fit, no padding
    }

    [Fact]
    public void FixedAlignmentCenter()
    {
        // Center align pads on both sides; an odd padding byte goes to the right
        Assert.Equal(SjisEncoding.GetBytes(" あ "), SjisEncoding.GetFixedBytes("あ", 4, FixedAlignment.Center));
        Assert.Equal(SjisEncoding.GetBytes(" あ  "), SjisEncoding.GetFixedBytes("あ", 5, FixedAlignment.Center));   // odd padding => extra byte on the right
        Assert.Equal(SjisEncoding.GetBytes("  あ  "), SjisEncoding.GetFixedBytes("あ", 6, FixedAlignment.Center));
        Assert.Equal(SjisEncoding.GetBytes("あA "), SjisEncoding.GetFixedBytes("あA", 4, FixedAlignment.Center));
        Assert.Equal(SjisEncoding.GetBytes(" あA "), SjisEncoding.GetFixedBytes("あA", 5, FixedAlignment.Center));
        Assert.Equal(SjisEncoding.GetBytes(" あA  "), SjisEncoding.GetFixedBytes("あA", 6, FixedAlignment.Center));
        Assert.Equal(SjisEncoding.GetBytes("Aあ "), SjisEncoding.GetFixedBytes("Aあ", 4, FixedAlignment.Center));
        Assert.Equal(SjisEncoding.GetBytes(" Aあ "), SjisEncoding.GetFixedBytes("Aあ", 5, FixedAlignment.Center));
        Assert.Equal(SjisEncoding.GetBytes(" Aあ  "), SjisEncoding.GetFixedBytes("Aあ", 6, FixedAlignment.Center));
        Assert.Equal(SjisEncoding.GetBytes("A "), SjisEncoding.GetFixedBytes("Aあ", 2, FixedAlignment.Center));   // 2-byte あ does not fit in the remaining 1 byte
    }

    [Fact]
    public void Fill()
    {
        // Left align: single-byte "A" with the remaining width filled by the padding
        Assert.Equal(SjisEncoding.GetBytes("A "), SjisEncoding.GetFixedBytes("A", 2, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A  "), SjisEncoding.GetFixedBytes("A", 3, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A   "), SjisEncoding.GetFixedBytes("A", 4, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A    "), SjisEncoding.GetFixedBytes("A", 5, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A     "), SjisEncoding.GetFixedBytes("A", 6, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A      "), SjisEncoding.GetFixedBytes("A", 7, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A       "), SjisEncoding.GetFixedBytes("A", 8, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A        "), SjisEncoding.GetFixedBytes("A", 9, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("A         "), SjisEncoding.GetFixedBytes("A", 10, FixedAlignment.Left));
    }

    [Fact]
    public void FixedAlignmentHankana()
    {
        // Half-width kana is a single byte in Shift-JIS, so ｱ(1) leaves padding within a fixed width
        Assert.Equal(SjisEncoding.GetBytes("ｱ  "), SjisEncoding.GetFixedBytes("ｱ", 3, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("ｱA  "), SjisEncoding.GetFixedBytes("ｱA", 4, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("Aｱ  "), SjisEncoding.GetFixedBytes("Aｱ", 4, FixedAlignment.Left));
        Assert.Equal(SjisEncoding.GetBytes("ｱあ "), SjisEncoding.GetFixedBytes("ｱあ", 4, FixedAlignment.Left));   // ｱ(1) + あ(2) + pad 1

        // Voiced half-width kana "ｶﾞ" is two single-byte characters (1 + 1)
        Assert.Equal(SjisEncoding.GetBytes("ｶﾞ  "), SjisEncoding.GetFixedBytes("ｶﾞ", 4, FixedAlignment.Left));

        // 2-byte あ does not fit in the remaining 1 byte
        Assert.Equal(SjisEncoding.GetBytes("ｱ "), SjisEncoding.GetFixedBytes("ｱあ", 2, FixedAlignment.Left));

        // Right / center alignment with half-width kana
        Assert.Equal(SjisEncoding.GetBytes("  ｱ"), SjisEncoding.GetFixedBytes("ｱ", 3, FixedAlignment.Right));
        Assert.Equal(SjisEncoding.GetBytes(" ｱ "), SjisEncoding.GetFixedBytes("ｱ", 3, FixedAlignment.Center));
    }

    [Fact]
    public void GetFixedBytesSpan()
    {
        // Span<byte> output overload (Left align, default padding)
        var left = new byte[4];
        SjisEncoding.GetFixedBytes("ｱあ", left, FixedAlignment.Left);
        Assert.Equal(SjisEncoding.GetBytes("ｱあ "), left);

        // Span<byte> output overload with a custom padding byte (Left align)
        var padded = new byte[4];
        SjisEncoding.GetFixedBytes("A", padded, (byte)'*');
        Assert.Equal(SjisEncoding.GetBytes("A***"), padded);

        // Span<byte> output overload with alignment and a custom padding byte
        var right = new byte[3];
        SjisEncoding.GetFixedBytes("A", right, FixedAlignment.Right, (byte)'*');
        Assert.Equal(SjisEncoding.GetBytes("**A"), right);

        // byte[]-returning custom padding overloads
        Assert.Equal(SjisEncoding.GetBytes("A***"), SjisEncoding.GetFixedBytes("A", 4, (byte)'*'));
        Assert.Equal(SjisEncoding.GetBytes("**A"), SjisEncoding.GetFixedBytes("A", 3, FixedAlignment.Right, (byte)'*'));
    }

    // Split

    [Fact]
    public void GetLimitString()
    {
        // "あAあAあA" (each あ is 2 bytes, each A is 1 byte)
        Assert.Equal(string.Empty, SjisEncoding.GetLimitString("あAあAあA", 0, 1));   // 2-byte あ does not fit in 1 byte
        Assert.Equal("あ", SjisEncoding.GetLimitString("あAあAあA", 0, 2));
        Assert.Equal("あA", SjisEncoding.GetLimitString("あAあAあA", 0, 3));
        Assert.Equal("あA", SjisEncoding.GetLimitString("あAあAあA", 0, 4));           // the next あ needs 2 bytes, only 1 is left
        Assert.Equal("あAあ", SjisEncoding.GetLimitString("あAあAあA", 0, 5));
        Assert.Equal("あAあA", SjisEncoding.GetLimitString("あAあAあA", 0, 6));
    }

    [Fact]
    public void GetLimitStringHankana()
    {
        // "ｱAあ" (1 + 1 + 2 bytes)
        Assert.Equal(string.Empty, SjisEncoding.GetLimitString("ｱAあ", 0, 0));
        Assert.Equal("ｱ", SjisEncoding.GetLimitString("ｱAあ", 0, 1));
        Assert.Equal("ｱA", SjisEncoding.GetLimitString("ｱAあ", 0, 2));
        Assert.Equal("ｱA", SjisEncoding.GetLimitString("ｱAあ", 0, 3));   // 2-byte あ does not fit in the remaining 1 byte
        Assert.Equal("ｱAあ", SjisEncoding.GetLimitString("ｱAあ", 0, 4));

        // Voiced half-width kana "ｶﾞ" is two single-byte characters
        Assert.Equal("ｶ", SjisEncoding.GetLimitString("ｶﾞ", 0, 1));
        Assert.Equal("ｶﾞ", SjisEncoding.GetLimitString("ｶﾞ", 0, 2));
    }

    [Fact]
    public void GetLimitStringByteCount()
    {
        // Mixed input "ｱAあ" (1 + 1 + 2 bytes)
        Assert.Equal(string.Empty, SjisEncoding.GetLimitString("ｱAあ", 0));
        Assert.Equal("ｱ", SjisEncoding.GetLimitString("ｱAあ", 1));
        Assert.Equal("ｱA", SjisEncoding.GetLimitString("ｱAあ", 2));
        Assert.Equal("ｱA", SjisEncoding.GetLimitString("ｱAあ", 3));    // 2-byte あ does not fit in the remaining 1 byte
        Assert.Equal("ｱAあ", SjisEncoding.GetLimitString("ｱAあ", 4));  // exact boundary
        Assert.Equal("ｱAあ", SjisEncoding.GetLimitString("ｱAあ", 5));  // over capacity

        Assert.Equal(string.Empty, SjisEncoding.GetLimitString(string.Empty, 4));
    }

    [Fact]
    public void GetLimitStringOffset()
    {
        // "ｱAあ" from offset 1 => "Aあ"
        Assert.Equal(string.Empty, SjisEncoding.GetLimitString("ｱAあ", 1, 0));
        Assert.Equal("A", SjisEncoding.GetLimitString("ｱAあ", 1, 1));
        Assert.Equal("A", SjisEncoding.GetLimitString("ｱAあ", 1, 2));    // 2-byte あ does not fit in the remaining 1 byte
        Assert.Equal("Aあ", SjisEncoding.GetLimitString("ｱAあ", 1, 3));  // exact boundary

        // "ｱAあ" from offset 2 => "あ"
        Assert.Equal(string.Empty, SjisEncoding.GetLimitString("ｱAあ", 2, 1));
        Assert.Equal("あ", SjisEncoding.GetLimitString("ｱAあ", 2, 2));
    }

    [Fact]
    public void GetLimitStringSpan()
    {
        Assert.Equal(string.Empty, SjisEncoding.GetLimitString("ｱAあ".AsSpan(), 0).ToString());
        Assert.Equal("ｱ", SjisEncoding.GetLimitString("ｱAあ".AsSpan(), 1).ToString());
        Assert.Equal("ｱA", SjisEncoding.GetLimitString("ｱAあ".AsSpan(), 2).ToString());
        Assert.Equal("ｱA", SjisEncoding.GetLimitString("ｱAあ".AsSpan(), 3).ToString());   // 2-byte あ does not fit in the remaining 1 byte
        Assert.Equal("ｱAあ", SjisEncoding.GetLimitString("ｱAあ".AsSpan(), 4).ToString()); // exact boundary
        Assert.Equal("ｱAあ", SjisEncoding.GetLimitString("ｱAあ".AsSpan(), 5).ToString());

        Assert.Equal(string.Empty, SjisEncoding.GetLimitString([], 4).ToString());
    }

    [Fact]
    public void SplitLimitString()
    {
        // Mixed input "ｱAあ" (1 + 1 + 2 bytes)
        Assert.Equal("ｱA|あ", Join(SjisEncoding.SplitLimitString("ｱAあ", 2)));
        Assert.Equal("ｱ|A", Join(SjisEncoding.SplitLimitString("ｱAあ", 1)));   // trailing 2-byte あ cannot fit in 1 byte and is dropped
        Assert.Equal("ｱAあ", Join(SjisEncoding.SplitLimitString("ｱAあ", 4)));   // whole string fits in a single segment
        Assert.Equal("ｱAあ", Join(SjisEncoding.SplitLimitString("ｱAあ", 5)));

        // Full-width + ASCII
        Assert.Equal("あA|あA|あA", Join(SjisEncoding.SplitLimitString("あAあAあA", 3)));

        // Even split
        Assert.Equal("AA|AA", Join(SjisEncoding.SplitLimitString("AAAA", 2)));

        // A single 2-byte character cannot fit into 1 byte
        Assert.Equal(string.Empty, Join(SjisEncoding.SplitLimitString("あ", 1)));

        // Empty input
        Assert.Equal(string.Empty, Join(SjisEncoding.SplitLimitString(string.Empty, 2)));
    }

    [Fact]
    public void SplitLimitStringOffset()
    {
        // ReadOnlySpan<char> overload
        Assert.Equal("ｱA|あ", Join(SjisEncoding.SplitLimitString("ｱAあ".AsSpan(), 2)));

        // string + offset overload ("ｱAあ" from offset 1 => "Aあ")
        Assert.Equal("A|あ", Join(SjisEncoding.SplitLimitString("ｱAあ", 1, 2)));

        // Offset equal to the length yields an empty split
        Assert.Equal(string.Empty, Join(SjisEncoding.SplitLimitString("ｱAあ", 3, 2)));

        // Offset past the length throws
        Assert.Throws<ArgumentOutOfRangeException>(static () =>
        {
            SjisEncoding.SplitLimitString("ｱAあ", 4, 2);
        });
    }

    private static string Join(SjisSplitEnumerator enumerator)
    {
        List<string> result = [];
        foreach (var segment in enumerator)
        {
            result.Add(segment.ToString());
        }

        return string.Join('|', result);
    }
}
