namespace Smart.Text.Japanese
{
    using System.Text;

    using Xunit;

    public class SjisEncodingTest
    {
        private static readonly Encoding Enc = SjisEncoding.Instance;

        public SjisEncodingTest()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [Fact]
        public void PaddingLeft()
        {
            Assert.Equal(Enc.GetBytes("あ "), Enc.GetFixedBytes("あ", 3, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("あA "), Enc.GetFixedBytes("あA", 4, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("Aあ "), Enc.GetFixedBytes("Aあ", 4, FixedAlignment.Left));
        }

        [Fact]
        public void PaddingRight()
        {
            Assert.Equal(Enc.GetBytes(" あ"), Enc.GetFixedBytes("あ", 3, FixedAlignment.Right));
            Assert.Equal(Enc.GetBytes(" あA"), Enc.GetFixedBytes("あA", 4, FixedAlignment.Right));
            Assert.Equal(Enc.GetBytes(" Aあ"), Enc.GetFixedBytes("Aあ", 4, FixedAlignment.Right));
        }

        [Fact]
        public void PaddingCenter()
        {
            Assert.Equal(Enc.GetBytes(" あ "), Enc.GetFixedBytes("あ", 4, FixedAlignment.Center));
            Assert.Equal(Enc.GetBytes(" あ  "), Enc.GetFixedBytes("あ", 5, FixedAlignment.Center));
            Assert.Equal(Enc.GetBytes("  あ  "), Enc.GetFixedBytes("あ", 6, FixedAlignment.Center));
            Assert.Equal(Enc.GetBytes("あA "), Enc.GetFixedBytes("あA", 4, FixedAlignment.Center));
            Assert.Equal(Enc.GetBytes(" あA "), Enc.GetFixedBytes("あA", 5, FixedAlignment.Center));
            Assert.Equal(Enc.GetBytes(" あA  "), Enc.GetFixedBytes("あA", 6, FixedAlignment.Center));
            Assert.Equal(Enc.GetBytes("Aあ "), Enc.GetFixedBytes("Aあ", 4, FixedAlignment.Center));
            Assert.Equal(Enc.GetBytes(" Aあ "), Enc.GetFixedBytes("Aあ", 5, FixedAlignment.Center));
            Assert.Equal(Enc.GetBytes(" Aあ  "), Enc.GetFixedBytes("Aあ", 6, FixedAlignment.Center));
        }

        [Fact]
        public void Fill()
        {
            Assert.Equal(Enc.GetBytes("A "), Enc.GetFixedBytes("A", 2, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("A  "), Enc.GetFixedBytes("A", 3, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("A   "), Enc.GetFixedBytes("A", 4, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("A    "), Enc.GetFixedBytes("A", 5, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("A     "), Enc.GetFixedBytes("A", 6, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("A      "), Enc.GetFixedBytes("A", 7, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("A       "), Enc.GetFixedBytes("A", 8, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("A        "), Enc.GetFixedBytes("A", 9, FixedAlignment.Left));
            Assert.Equal(Enc.GetBytes("A         "), Enc.GetFixedBytes("A", 10, FixedAlignment.Left));
        }
    }
}
