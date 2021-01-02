namespace Smart.Text.Japanese
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class EncodingExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static unsafe void Fill(byte* pArray, int offset, int length, byte value)
        {
            pArray[offset] = value;

            int copy;
            for (copy = 1; copy <= length >> 1; copy <<= 1)
            {
                Buffer.MemoryCopy(&pArray[offset], &pArray[offset + copy], copy, copy);
            }

            var left = length - copy;
            if (left > 0)
            {
                Buffer.MemoryCopy(&pArray[offset], &pArray[offset + copy], copy, left);
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static unsafe byte[] GetFixedBytes(this Encoding enc, string str, int offset, int length, int byteCount, FixedAlignment alignment, byte padding)
        {
            if (String.IsNullOrEmpty(str))
            {
                var buffer = new byte[byteCount];

                fixed (byte* pBytes = &buffer[0])
                {
                    Fill(pBytes, 0, byteCount, padding);
                }

                return buffer;
            }

            var bytes = new byte[byteCount];
            fixed (byte* pBytes = &bytes[0])
            fixed (char* pString = str)
            {
                var count = enc.GetByteCount(&pString[offset], length);
                if (count == byteCount)
                {
                    enc.GetBytes(&pString[offset], length, pBytes, count);
                }
                else
                {
                    if (alignment == FixedAlignment.Left)
                    {
                        enc.GetBytes(pString + offset, length, pBytes, count);
                        Fill(pBytes, count, byteCount - count, padding);
                    }
                    else if (alignment == FixedAlignment.Right)
                    {
                        var fillLength = byteCount - count;
                        enc.GetBytes(pString + offset, length, pBytes + fillLength, count);
                        Fill(pBytes, 0, fillLength, padding);
                    }
                    else
                    {
                        var half = (byteCount - count) / 2;
                        Fill(pBytes, 0, half, padding);
                        enc.GetBytes(pString + offset, length, pBytes + half, count);
                        Fill(pBytes, half + count, byteCount - half - count, padding);
                    }
                }
            }

            return bytes;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(this Encoding enc, string str, int offset, int length, int byteCount, FixedAlignment alignment) =>
            enc.GetFixedBytes(str, offset, length, byteCount, alignment, 0x20);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(this Encoding enc, string str, int offset, int length, int byteCount, byte padding) =>
            enc.GetFixedBytes(str, offset, length, byteCount, FixedAlignment.Left, padding);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(this Encoding enc, string str, int length, int byteCount, FixedAlignment alignment, byte padding) =>
            enc.GetFixedBytes(str, 0, length, byteCount, alignment, padding);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(this Encoding enc, string str, int length, int byteCount, FixedAlignment alignment) =>
            enc.GetFixedBytes(str, 0, length, byteCount, alignment, 0x20);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(this Encoding enc, string str, int length, int byteCount, byte padding) =>
            enc.GetFixedBytes(str, 0, length, byteCount, FixedAlignment.Left, padding);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(this Encoding enc, string str, int byteCount, FixedAlignment alignment, byte padding) =>
            enc.GetFixedBytes(str, 0, str.Length, byteCount, alignment, padding);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(this Encoding enc, string str, int byteCount, FixedAlignment alignment) =>
            enc.GetFixedBytes(str, 0, str.Length, byteCount, alignment, 0x20);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte[] GetFixedBytes(this Encoding enc, string str, int byteCount, byte padding) =>
            enc.GetFixedBytes(str, 0, str.Length, byteCount, FixedAlignment.Left, padding);
    }
}
