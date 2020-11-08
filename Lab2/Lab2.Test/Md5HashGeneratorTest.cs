using System.IO;
using Lab2.Core;
using Xunit;

namespace Lab2.Test
{
    public class Md5HashGeneratorTest
    {
        private readonly Md5HashGenerator _generator;

        public Md5HashGeneratorTest()
        {
            _generator = new Md5HashGenerator();
        }

        [Theory]
        [InlineData("", "D41D8CD98F00B204E9800998ECF8427E")]
        [InlineData("a", "0CC175B9C0F1B6A831C399E269772661")]
        [InlineData("abc", "900150983CD24FB0D6963F7D28E17F72")]
        [InlineData("message digest", "F96B697D7CB7938D525A2F31AAF161D0")]
        [InlineData("abcdefghijklmnopqrstuvwxyz", "C3FCD3D76192E4007DFB496CCA67E13B")]
        [InlineData("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", "D174AB98D277D9F5A5611C2C9F419D9F")]
        [InlineData("12345678901234567890123456789012345678901234567890123456789012345678901234567890", "57EDF4A22BE3C955AC49DA2E2107B67A")]
        public void Hash_Positive_ReturnsHash(string input, string expected)
        {
            var actual = _generator.Hash(input);

            Assert.Equal(expected, actual);
        } 
        
        [Theory]
        [InlineData(@"D:\123Md5Test.txt", "57EDF4A22BE3C955AC49DA2E2107B67A")]
        [InlineData(@"D:\Films\The.Best.Years.Of.Our.Lives.1946.x264.BDRip.(1080p).mkv", "D3E6193E23C60CD19E41BBF4BBFD7BE1")]
        public void Hash_File_ReturnsHash(string filePath, string expected)
        {
            var actual = _generator.Hash(new BinaryReader(File.OpenRead(filePath)));

            Assert.Equal(expected, actual);
        }
    }
}
