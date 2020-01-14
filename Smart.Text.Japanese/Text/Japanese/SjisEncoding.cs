namespace Smart.Text.Japanese
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class SjisEncoding
    {
        private static readonly Lazy<Encoding> Provider = new Lazy<Encoding>(() => Encoding.GetEncoding("Shift_JIS"));

        public static Encoding Instance => Provider.Value;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int GetByteCount(string str)
        {
            return Instance.GetByteCount(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetBytes(string str)
        {
            return Instance.GetBytes(str);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string GetString(byte[] bytes, int index, int count)
        {
            return Instance.GetString(bytes, index, count);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsSingleByte(char c)
        {
            return (c <= 128) || ((c >= 0xF8F0) && (c <= 0xF8F3));
        }

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
        public static IEnumerable<string> SplitLimitString(string str, int byteCount)
        {
            return SplitLimitString(str, 0, byteCount);
        }

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