﻿namespace Smart.Text.Japanese
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    ///
    /// </summary>
    public static class SjisEncoding
    {
        private static readonly Lazy<Encoding> Provider = new Lazy<Encoding>(() => Encoding.GetEncoding("Shift_JIS"));

        /// <summary>
        ///
        /// </summary>
        public static Encoding Instance => Provider.Value;

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static int GetByteCount(string str)
        {
            return Instance.GetByteCount(str);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] GetBytes(string str)
        {
            return Instance.GetBytes(str);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="index"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string GetString(byte[] bytes, int index, int count)
        {
            return Instance.GetString(bytes, index, count);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="alignment"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static byte[] GetFixedBytes(string str, int length, FixedAlignment alignment, byte padding)
        {
            var bytes = Instance.GetBytes(str);
            if (bytes.Length == length)
            {
                return bytes;
            }

            if (bytes.Length > length)
            {
                // Truncate
                var newBytes = new byte[length];
                if (((bytes[length - 1] >= 0x81) && (bytes[length - 1] <= 0x9f)) ||
                    ((bytes[length - 1] >= 0xe0) && (bytes[length - 1] <= 0xfc)))
                {
                    Buffer.BlockCopy(bytes, 0, newBytes, 0, length - 1);
                    newBytes[length - 1] = padding;
                }
                else
                {
                    Buffer.BlockCopy(bytes, 0, newBytes, 0, length);
                }

                return newBytes;
            }
            else
            {
                // Padding
                var newBytes = new byte[length];
                if (alignment == FixedAlignment.Left)
                {
                    Buffer.BlockCopy(bytes, 0, newBytes, 0, bytes.Length);
                    for (var i = bytes.Length; i < length; i++)
                    {
                        newBytes[i] = padding;
                    }
                }
                else if (alignment == FixedAlignment.Right)
                {
                    var i = 0;
                    for (; i < length - bytes.Length; i++)
                    {
                        newBytes[i] = padding;
                    }

                    Buffer.BlockCopy(bytes, 0, newBytes, i, bytes.Length);
                }
                else
                {
                    var i = 0;
                    for (; i < (length - bytes.Length) / 2; i++)
                    {
                        newBytes[i] = padding;
                    }

                    Buffer.BlockCopy(bytes, 0, newBytes, i, bytes.Length);
                    for (i = i + bytes.Length; i < length; i++)
                    {
                        newBytes[i] = padding;
                    }
                }

                return newBytes;
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="alignment"></param>
        /// <returns></returns>
        public static byte[] GetFixedBytes(string str, int length, FixedAlignment alignment)
        {
            return GetFixedBytes(str, length, alignment, 0x20);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <param name="padding"></param>
        /// <returns></returns>
        public static byte[] GetFixedBytes(string str, int length, byte padding)
        {
            return GetFixedBytes(str, length, FixedAlignment.Left, padding);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static byte[] GetFixedBytes(string str, int length)
        {
            return GetFixedBytes(str, length, FixedAlignment.Left, 0x20);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string GetLimitString(string str, int length)
        {
            var bytes = Instance.GetBytes(str);
            if (bytes.Length <= length)
            {
                return str;
            }

            if (((bytes[length - 1] >= 0x81) && (bytes[length - 1] <= 0x9f)) ||
                ((bytes[length - 1] >= 0xe0) && (bytes[length - 1] <= 0xfc)))
            {
                length = length - 1;
            }

            return Instance.GetString(bytes, 0, length);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string[] SplitLimitString(string str, int length)
        {
            var bytes = Instance.GetBytes(str);
            if (bytes.Length <= length)
            {
                return new[] { str };
            }

            var list = new List<string>();
            var start = 0;
            while (start < bytes.Length)
            {
                var left = bytes.Length - start;
                int count;
                if (left < length)
                {
                    count = left;
                }
                else
                {
                    var offset = start + length - 1;
                    count = ((bytes[offset] >= 0x81) && (bytes[offset] <= 0x9f)) ||
                            ((bytes[offset] >= 0xe0) && (bytes[offset] <= 0xfc))
                        ? length - 1
                        : length;
                }

                list.Add(Instance.GetString(bytes, start, count));
                start += count;
            }

            return list.ToArray();
        }
    }
}
