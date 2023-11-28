namespace Smart.Text.Japanese;

using System.Runtime.CompilerServices;
using System.Text;

public static class SjisEncoding
{
#pragma warning disable IDE0032
    private static readonly Encoding Encoding = Encoding.GetEncoding("Shift_JIS");

    public static Encoding Instance
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Encoding;
    }
#pragma warning restore IDE0032

    // Shortcut

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetByteCount(char[] chars) => Encoding.GetByteCount(chars);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetByteCount(string s) => Encoding.GetByteCount(s);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetByteCount(char[] chars, int index, int count) => Encoding.GetByteCount(chars, index, count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetByteCount(string s, int index, int count) => Encoding.GetByteCount(s, index, count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static unsafe int GetByteCount(char* chars, int count) => Encoding.GetByteCount(chars, count);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int GetByteCount(ReadOnlySpan<char> chars) => Encoding.GetByteCount(chars);

    public static byte[] GetBytes(char[] chars) => Encoding.GetBytes(chars);

    public static byte[] GetBytes(char[] chars, int index, int count) => Encoding.GetBytes(chars, index, count);

    public static int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex) => Encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);

    public static byte[] GetBytes(string s) => Encoding.GetBytes(s);

    public static byte[] GetBytes(string s, int index, int count) => Encoding.GetBytes(s, index, count);

    public static int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex) => Encoding.GetBytes(s, charIndex, charCount, bytes, byteIndex);

    public static unsafe int GetBytes(char* chars, int charCount, byte* bytes, int byteCount) => Encoding.GetBytes(chars, charCount, bytes, byteCount);

    public static int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes) => Encoding.GetBytes(chars, bytes);

    public static int GetCharCount(byte[] bytes) => Encoding.GetCharCount(bytes);

    public static int GetCharCount(byte[] bytes, int index, int count) => Encoding.GetCharCount(bytes, index, count);

    public static unsafe int GetCharCount(byte* bytes, int count) => Encoding.GetCharCount(bytes, count);

    public static int GetCharCount(ReadOnlySpan<byte> bytes) => Encoding.GetCharCount(bytes);

    public static char[] GetChars(byte[] bytes) => Encoding.GetChars(bytes);

    public static char[] GetChars(byte[] bytes, int index, int count) => Encoding.GetChars(bytes, index, count);

    public static int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex) => Encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);

    public static unsafe int GetChars(byte* bytes, int byteCount, char* chars, int charCount) => Encoding.GetChars(bytes, byteCount, chars, charCount);

    public static int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars) => Encoding.GetChars(bytes, chars);

    public static unsafe string GetString(byte* bytes, int byteCount) => Encoding.GetString(bytes, byteCount);

    public static string GetString(ReadOnlySpan<byte> bytes) => Encoding.GetString(bytes);

    public static string GetString(byte[] bytes) => Encoding.GetString(bytes);

    public static string GetString(byte[] bytes, int index, int count) => Encoding.GetString(bytes, index, count);

    // Calc

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsSingleByte(char c) => (c <= 128) || ((c >= 0xF8F0) && (c <= 0xF8F3));

    // Fixed bytes

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SkipLocalsInit]
    public static byte[] GetFixedBytes(ReadOnlySpan<char> chars, int byteCount, FixedAlignment alignment)
    {
        var bytes = new byte[byteCount];
        GetFixedBytes(chars, bytes, alignment, 0x20);
        return bytes;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SkipLocalsInit]
    public static byte[] GetFixedBytes(ReadOnlySpan<char> chars, int byteCount, byte padding)
    {
        var bytes = new byte[byteCount];
        GetFixedBytes(chars, bytes, FixedAlignment.Left, padding);
        return bytes;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [SkipLocalsInit]
    public static byte[] GetFixedBytes(ReadOnlySpan<char> chars, int byteCount, FixedAlignment alignment, byte padding)
    {
        var bytes = new byte[byteCount];
        GetFixedBytes(chars, bytes, alignment, padding);
        return bytes;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetFixedBytes(ReadOnlySpan<char> chars, Span<byte> bytes, FixedAlignment alignment) =>
        GetFixedBytes(chars, bytes, alignment, 0x20);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static void GetFixedBytes(ReadOnlySpan<char> chars, Span<byte> bytes, byte padding) =>
        GetFixedBytes(chars, bytes, FixedAlignment.Left, padding);

    public static unsafe void GetFixedBytes(ReadOnlySpan<char> chars, Span<byte> bytes, FixedAlignment alignment, byte padding)
    {
        if (chars.IsEmpty)
        {
            bytes.Fill(padding);
            return;
        }

        var sourceLength = 0;
        var byteCount = 0;
        while (sourceLength < chars.Length)
        {
            var size = IsSingleByte(chars[sourceLength]) ? 1 : 2;
            if (byteCount + size > bytes.Length)
            {
                break;
            }

            sourceLength++;
            byteCount += size;
        }

        fixed (byte* pBytes = bytes)
        fixed (char* pString = chars)
        {
            if (byteCount == bytes.Length)
            {
                Encoding.GetBytes(pString, sourceLength, pBytes, bytes.Length);
            }
            else
            {
                if (alignment == FixedAlignment.Left)
                {
                    Encoding.GetBytes(pString, sourceLength, pBytes, byteCount);
                    bytes[byteCount..].Fill(padding);
                }
                else if (alignment == FixedAlignment.Right)
                {
                    var fillLength = bytes.Length - byteCount;
                    Encoding.GetBytes(pString, sourceLength, pBytes + fillLength, byteCount);
                    bytes[..fillLength].Fill(padding);
                }
                else
                {
                    var half = (bytes.Length - byteCount) / 2;
                    bytes[..half].Fill(padding);
                    Encoding.GetBytes(pString, sourceLength, pBytes + half, byteCount);
                    bytes[(half + byteCount)..bytes.Length].Fill(padding);
                }
            }
        }
    }

    // Split

    private static int CalcLimitLength(ReadOnlySpan<char> chars, int limit)
    {
        var length = 0;
        var byteCount = 0;
        while (length < chars.Length)
        {
            byteCount += IsSingleByte(chars[length]) ? 1 : 2;
            if (byteCount > limit)
            {
                break;
            }

            length++;
        }

        return length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetLimitString(string str, int byteCount)
    {
        return String.IsNullOrEmpty(str) ? str : str[..CalcLimitLength(str, byteCount)];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetLimitString(string str, int offset, int byteCount)
    {
        return String.IsNullOrEmpty(str) ? str : str.Substring(offset, CalcLimitLength(str.AsSpan(offset), byteCount));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static IEnumerable<string> SplitLimitString(string str, int byteCount) => SplitLimitString(str, 0, byteCount);

#pragma warning disable CA1062
    public static IEnumerable<string> SplitLimitString(string str, int offset, int byteCount)
    {
        while (true)
        {
            var length = CalcLimitLength(str.AsSpan(offset), byteCount);
            if (length == 0)
            {
                break;
            }

            yield return str.Substring(offset, length);

            offset += length;
        }
    }
#pragma warning restore CA1062
}
