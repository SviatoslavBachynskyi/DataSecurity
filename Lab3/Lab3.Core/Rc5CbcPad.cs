using Lab1.Core;
using Lab2.Core;
using System;
using System.IO;
using System.Linq;
using static Lab3.Core.Rc5Constants;

namespace Lab3.Core
{
    public class Rc5CbcPad
    {
        private Md5HashGenerator hashGenerator = new Md5HashGenerator();

        public void Encrypt(BinaryReader input, string keyPhrase, BinaryWriter output)
        {

        }

        private byte[] GenerateVector()
        {
            var byteCount = 2 * U;
            var constants = new LcConstants((uint)Environment.TickCount, 16807, 28657, 2147483648);
            var stringWriter = new StringWriter();
            var vector = LcPseudoRandomGenerator.Generate(constants, null, stringWriter, (uint)byteCount / 4);
            var stringNumbers = stringWriter.ToString();
            var numbers = stringNumbers.Split(", ").Select(UInt32.Parse).ToArray();
            var result = new byte[2 * W];

            Buffer.BlockCopy(numbers, 0, result, 0, byteCount);
            return result;
        }
    }
}
