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
