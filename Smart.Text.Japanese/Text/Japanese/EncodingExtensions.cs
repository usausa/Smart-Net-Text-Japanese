namespace Smart.Text.Japanese
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Text;

    public static class EncodingExtensions
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "Extensions")]
        public static unsafe byte[] GetFixedBytes(this Encoding enc, string str, int offset, int length, int byteCount, FixedAlignment alignment, byte padding)
        {
            if (String.IsNullOrEmpty(str))
            {
                var buffer = new byte[byteCount];
                buffer.AsSpan().Fill(padding);
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
                        bytes.AsSpan(count, byteCount - count).Fill(padding);
                    }
                    else if (alignment == FixedAlignment.Right)
                    {
                        var fillLength = byteCount - count;
                        enc.GetBytes(pString + offset, length, pBytes + fillLength, count);
                        bytes.AsSpan(0, fillLength).Fill(padding);
                    }
                    else
                    {
                        var half = (byteCount - count) / 2;
                        bytes.AsSpan(0, half).Fill(padding);
                        enc.GetBytes(pString + offset, length, pBytes + half, count);
                        bytes.AsSpan(half + count, byteCount - half - count).Fill(padding);
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
