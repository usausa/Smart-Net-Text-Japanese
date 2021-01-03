namespace Smart.Text.Japanese
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class SjisEncoding
    {
        private static readonly Encoding Encoding = Encoding.GetEncoding("Shift_JIS");

        public static Encoding Instance
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get
            {
                return Encoding;
            }
        }

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

        // TODO buffer version
        // TODO pointer version ?

#if NET5_0
        [SkipLocalsInit]
#endif
        public static unsafe byte[] GetFixedBytes(ReadOnlySpan<char> source, int byteCount, FixedAlignment alignment, byte padding)
        {
            if (source.IsEmpty)
            {
                var buffer = new byte[byteCount];
                buffer.AsSpan().Fill(padding);
                return buffer;
            }

            var count = 0;
            var sourceLength = 0;
            while (sourceLength < source.Length)
            {
                var size = IsSingleByte(source[sourceLength]) ? 1 : 2;
                if (count + size > byteCount)
                {
                    break;
                }

                sourceLength++;
                count += size;
            }

            var bytes = new byte[byteCount];
            fixed (byte* pBytes = &bytes[0])
            fixed (char* pString = source)
            {
                if (count == byteCount)
                {
                    Encoding.GetBytes(pString, source.Length, pBytes, byteCount);
                }
                else
                {
                    if (alignment == FixedAlignment.Left)
                    {
                        Encoding.GetBytes(pString, sourceLength, pBytes, count);
                        bytes.AsSpan(count, byteCount - count).Fill(padding);
                    }
                    else if (alignment == FixedAlignment.Right)
                    {
                        var fillLength = byteCount - count;
                        Encoding.GetBytes(pString, sourceLength, pBytes + fillLength, count);
                        bytes.AsSpan(0, fillLength).Fill(padding);
                    }
                    else
                    {
                        var half = (byteCount - count) / 2;
                        bytes.AsSpan(0, half).Fill(padding);
                        Encoding.GetBytes(pString, sourceLength, pBytes + half, count);
                        bytes.AsSpan(half + count, byteCount - half - count).Fill(padding);
                    }
                }
            }

            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(ReadOnlySpan<char> source, int byteCount, FixedAlignment alignment) =>
            GetFixedBytes(source, byteCount, alignment, 0x20);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(ReadOnlySpan<char> source, int byteCount, byte padding) =>
            GetFixedBytes(source, byteCount, FixedAlignment.Left, padding);

        // Split

        // TODO rebuild: span, pointer

        private static int CalcLimitLength(string str, int offset, int byteCount)
        {
            var length = 0;
            var sjisCount = 0;
            for (var i = offset; i < str.Length; i++)
            {
                sjisCount += IsSingleByte(str[i]) ? 1 : 2;
                if (sjisCount > byteCount)
                {
                    break;
                }

                length++;

                if (sjisCount == byteCount)
                {
                    break;
                }
            }

            return length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLimitString(string str, int byteCount)
        {
            return String.IsNullOrEmpty(str) ? str : str.Substring(0, CalcLimitLength(str, 0, byteCount));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetLimitString(string str, int offset, int byteCount)
        {
            return String.IsNullOrEmpty(str) ? str : str.Substring(offset, CalcLimitLength(str, offset, byteCount));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEnumerable<string> SplitLimitString(string str, int byteCount) => SplitLimitString(str, 0, byteCount);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static IEnumerable<string> SplitLimitString(string str, int offset, int byteCount)
        {
            while (true)
            {
                var length = CalcLimitLength(str, offset, byteCount);
                if (length == 0)
                {
                    break;
                }

                yield return str.Substring(offset, length);

                offset += length;
            }
        }
    }
}