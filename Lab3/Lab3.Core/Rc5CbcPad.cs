using System;
using System.IO;
using System.Linq;
using Lab1.Core;
using Lab2.Core;

namespace Lab3.Core
{
    public class Rc5CbcPad
    {
        private const int W = 64;
        private const int R = 16;
        private const int B = 32;
        private const int HashSize = 16;
        private Md5HashGenerator hashGenerator = new Md5HashGenerator();

        public void Encrypt(BinaryReader input, string keyPhrase, BinaryWriter output)
        {
            
        }

        private byte[] GenerateVector()
        {
            var byteCount = 2 * W / 8;
            var constants = new LcConstants((uint)Environment.TickCount, 16807, 28657, 2147483648);
            var stringWriter = new StringWriter();
            var vector = LcPseudoRandomGenerator.Generate(constants, null, stringWriter, (uint) byteCount / 4);
            var stringNumbers = stringWriter.ToString();
            var numbers = stringNumbers.Split(", ").Select(s => UInt32.Parse(s)).ToArray();
            var result = new byte[2 * W];

            Buffer.BlockCopy(numbers,0, result, 0, byteCount);
            return result;
        }

        private byte[] GenerateKey(string keyPhrase)
        {
            var endBlock = hashGenerator.HashBytes(keyPhrase);
            var startBlock = hashGenerator.HashBytes(new BinaryReader(new MemoryStream(endBlock)));
            var result = new byte[B];

            Buffer.BlockCopy(startBlock,0, result, 0, HashSize);
            Buffer.BlockCopy(endBlock,0, result, HashSize, HashSize);
            return result;
        }
    }
}
