namespace Smart.Text.Japanese
{
    using System.Text;

    using Xunit;

    public class SjisEncodingTest
    {
        public SjisEncodingTest()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        // Fixed bytes

        [Fact]
        public void FixedAlignmentLeft()
        {
            Assert.Equal(SjisEncoding.GetBytes("あ "), SjisEncoding.GetFixedBytes("あ", 3, FixedAlignment.Left));
            Assert.Equal(SjisEncoding.GetBytes("あA "), SjisEncoding.GetFixedBytes("あA", 4, FixedAlignment.Left));
            Assert.Equal(SjisEncoding.GetBytes("Aあ "), SjisEncoding.GetFixedBytes("Aあ", 4, FixedAlignment.Left));
            Assert.Equal(SjisEncoding.GetBytes("A "), SjisEncoding.GetFixedBytes("Aあ", 2, FixedAlignment.Left));
        }

        [Fact]
        public void FixedAlignmentRight()
        {
            Assert.Equal(SjisEncoding.GetBytes(" あ"), SjisEncoding.GetFixedBytes("あ", 3, FixedAlignment.Right));
            Assert.Equal(SjisEncoding.GetBytes(" あA"), SjisEncoding.GetFixedBytes("あA", 4, FixedAlignment.Right));
            Assert.Equal(SjisEncoding.GetBytes(" Aあ"), SjisEncoding.GetFixedBytes("Aあ", 4, FixedAlignment.Right));
            Assert.Equal(SjisEncoding.GetBytes("あ"), SjisEncoding.GetFixedBytes("あ", 2, FixedAlignment.Right));
        }

        [Fact]
        public void FixedAlignmentCenter()
        {
            Assert.Equal(SjisEncoding.GetBytes(" あ "), SjisEncoding.GetFixedBytes("あ", 4, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes(" あ  "), SjisEncoding.GetFixedBytes("あ", 5, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes("  あ  "), SjisEncoding.GetFixedBytes("あ", 6, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes("あA "), SjisEncoding.GetFixedBytes("あA", 4, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes(" あA "), SjisEncoding.GetFixedBytes("あA", 5, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes(" あA  "), SjisEncoding.GetFixedBytes("あA", 6, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes("Aあ "), SjisEncoding.GetFixedBytes("Aあ", 4, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes(" Aあ "), SjisEncoding.GetFixedBytes("Aあ", 5, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes(" Aあ  "), SjisEncoding.GetFixedBytes("Aあ", 6, FixedAlignment.Center));
            Assert.Equal(SjisEncoding.GetBytes("A "), SjisEncoding.GetFixedBytes("Aあ", 2, FixedAlignment.Center));
        }

        [Fact]
        public void Fill()
        {
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

        // Split

        [Fact]
        public void GetLimitString()
        {
            Assert.Equal(string.Empty, SjisEncoding.GetLimitString("あAあAあA", 0, 1));
            Assert.Equal("あ", SjisEncoding.GetLimitString("あAあAあA", 0, 2));
            Assert.Equal("あA", SjisEncoding.GetLimitString("あAあAあA", 0, 3));
            Assert.Equal("あA", SjisEncoding.GetLimitString("あAあAあA", 0, 4));
            Assert.Equal("あAあ", SjisEncoding.GetLimitString("あAあAあA", 0, 5));
            Assert.Equal("あAあA", SjisEncoding.GetLimitString("あAあAあA", 0, 6));
        }
    }
}
