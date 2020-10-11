using System;
using System.IO;
using System.Linq;
using System.Text;
using Lab1.Core;
using Moq;
using Xunit;

namespace Lab1.Test
{
    public class LcPseudoRandomGeneratorTest
    {
        private static readonly LcConstants _variant1 = new LcConstants(2, 32, 0, 1023);

        private static readonly LcConstants _variant2 = new LcConstants(4, 243, 1, 2047);

        private static readonly LcConstants _variant3 = new LcConstants(8, 1024, 2, 14095);

        private static readonly LcConstants _variant22 = new LcConstants(31, 16807, 17711, 2147483647);
        
        private static readonly LcConstants _variant23 = new LcConstants(33, 65536, 28657, 2147483648);

        private readonly MemoryStream _memoryResult = new MemoryStream();

        private readonly StreamWriter _streamWriter; 
            
        private readonly StringBuilder _stringResult = new StringBuilder();

        private readonly StringWriter _stringWriter;

        public LcPseudoRandomGeneratorTest()
        {
            _stringWriter = new StringWriter(_stringResult);
            _streamWriter = new StreamWriter(_memoryResult);
        }

        public static readonly object[][] StringWriterTestData = {
            new object[] { _variant1, 4, "2, 64, 2, 64" },
            new object[] { _variant2, 4, "4, 973, 1035, 1772" },
            new object[] { _variant3, 10, "8, 8194, 4133, 3694, 5198, 8939, 5883, 5629, 13338, 59" },
            new object[] { _variant22, 15, "31, 538728, 464484619, 489952399, 1177685106, 2144303501, 238395064, 1648856704, 1205660951, 2035411723, 1897833109, 306471783, 1205489086, 1294360315, 304487806" },
            new object[] { _variant23, 20, "33, 2191345, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809, " +
                                           "1878093809, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809, 1878093809" },
        };

        [Theory]
        [MemberData(nameof(StringWriterTestData))]
        public void LcPseudoRandomGenerator_PositiveCase_WritesToStringWriter(LcConstants constants, uint count, string expected)
        {
            LcPseudoRandomGenerator.Generate(constants, null, _stringWriter, count);
            _stringWriter.Close();

            Assert.Equal(expected, _stringResult.ToString());
        }

        [Fact]
        public void LcPseudoRandomGenerator_Variant1_WritesToStreamWriter()
        {
            LcPseudoRandomGenerator.Generate(_variant1, _streamWriter);
            _streamWriter.Close();

            Assert.Equal("2, 64", new String(_memoryResult.ToArray().Select(b => (char) b).ToArray()));
        }

        public static readonly object[][] LastNumberTestData = {
            new object[] { _variant1, 2, ", 64" },
            new object[] { _variant2, 88, ", 278" },
            new object[] { _variant3, 2818, ", 1404" },
            new object[] { _variant22, 2147483646, ", 1606108730" },
            new object[] { _variant23, 3, ", 1878093809" },
        };

        [Theory]
        [MemberData(nameof(LastNumberTestData))]
        public void LcPseudoRandomGenerator_PositiveCase_WritesLastNumberToStreamWriter(LcConstants variant, uint expectedPeriod, string expectedLastNumber)
        {
            var streamWriterMock = new StreamWriterMock(_memoryResult);

            var actualPeriod = LcPseudoRandomGenerator.Generate(variant, streamWriterMock);

            Assert.Equal(expectedLastNumber, streamWriterMock.LastWriteCall);
            Assert.Equal(expectedPeriod, actualPeriod);
        }

        [Fact]
        public void LcPseudoRandomGenerator_Variant1CountHigherThanPeriod_WritesToBothStreamAndStringWriter()
        {
            LcPseudoRandomGenerator.Generate(_variant1, _streamWriter, _stringWriter, 10);
            _streamWriter.Close();
            _stringWriter.Close();

            Assert.Equal("2, 64, 2, 64, 2, 64, 2, 64, 2, 64", _stringResult.ToString());
            Assert.Equal("2, 64", new String(_memoryResult.ToArray().Select(b => (char)b).ToArray()));
        }

        [Fact]
        public void LcPseudoRandomGenerator_Variant1CountLowerThanPeriod_WritesToBothStreamAndStringWriter()
        {
            LcPseudoRandomGenerator.Generate(_variant1, _streamWriter, _stringWriter, 1);
            _streamWriter.Close();
            _stringWriter.Close();

            Assert.Equal("2", _stringResult.ToString());
            Assert.Equal("2, 64", new String(_memoryResult.ToArray().Select(b => (char)b).ToArray()));
        }
    }
}
