namespace Smart.Text.Japanese
{
    using System;

    /// <summary>
    ///
    /// </summary>
    [Flags]
    public enum KanaOption
    {
        /// <summary>
        /// r
        /// </summary>
        RomanToNarrow = 0x00000001,

        /// <summary>
        /// R
        /// </summary>
        RomanToWide = 0x00000002,

        /// <summary>
        /// n
        /// </summary>
        NumericToNarrow = 0x00000010,

        /// <summary>
        /// N
        /// </summary>
        NumericToWide = 0x00000020,

        /// <summary>
        /// a
        /// </summary>
        AsciiToNarrow = 0x00000100,

        /// <summary>
        /// A
        /// </summary>
        AsciiToWide = 0x00000200,

        /// <summary>
        /// s
        /// </summary>
        SpaceToNarrow = 0x00001000,

        /// <summary>
        /// S
        /// </summary>
        SpaceToWide = 0x00002000,

        /// <summary>
        /// k
        /// </summary>
        KanaToNarrow = 0x00010000,

        /// <summary>
        /// K
        /// </summary>
        KanaToWide = 0x00020000,

        /// <summary>
        /// h
        /// </summary>
        HiraganaToHankana = 0x00100000,

        /// <summary>
        /// H
        /// </summary>
        HankanaToHiragana = 0x00200000,

        /// <summary>
        /// c
        /// </summary>
        KatakanaToHiragana = 0x01000000,

        /// <summary>
        /// C
        /// </summary>
        HiraganaToKatakana = 0x02000000,

        /// <summary>
        ///
        /// </summary>
        Wide = RomanToWide | NumericToWide | AsciiToNarrow | KanaToWide,

        /// <summary>
        ///
        /// </summary>
        Narrow = RomanToNarrow | NumericToNarrow | AsciiToNarrow | KanaToNarrow,
    }
}
