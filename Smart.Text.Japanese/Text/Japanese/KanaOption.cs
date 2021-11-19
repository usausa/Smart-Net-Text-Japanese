namespace Smart.Text.Japanese;

using System;

[Flags]
public enum KanaOption
{
    // r
    RomanToNarrow = 0x00000001,
    // R
    RomanToWide = 0x00000002,

    // n
    NumericToNarrow = 0x00000010,
    // N
    NumericToWide = 0x00000020,

    // a
    AsciiToNarrow = 0x00000100,
    // A
    AsciiToWide = 0x00000200,

    // s
    SpaceToNarrow = 0x00001000,
    // S
    SpaceToWide = 0x00002000,

    // k
    KatakanaToHankana = 0x00010000,
    // K
    HankanaToKatakana = 0x00020000,

    // h
    HiraganaToHankana = 0x00100000,
    // H
    HankanaToHiragana = 0x00200000,

    // c
    KatakanaToHiragana = 0x01000000,
    // C
    HiraganaToKatakana = 0x02000000,

    Wide = RomanToWide | NumericToWide | AsciiToWide | SpaceToWide | HankanaToKatakana,
    Narrow = RomanToNarrow | NumericToNarrow | AsciiToNarrow | SpaceToNarrow | KatakanaToHankana
}
