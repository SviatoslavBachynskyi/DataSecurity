using System;
using System.IO;
using System.Linq;
using Lab1.Core;
using Lab2.Core;
using static Lab3.Core.Rc5Constants;
using static Lab3.Core.BinaryOperators;

namespace Lab3.Core
{
    public class Rc5CbcPad
    {
        private Md5HashGenerator hashGenerator = new Md5HashGenerator();
        private readonly int U = W / 8;
        private readonly int SArraySize = 2 * R + 2;

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
            var numbers = stringNumbers.Split(", ").Select(s => UInt32.Parse(s)).ToArray();
            var result = new byte[2 * W];

            Buffer.BlockCopy(numbers, 0, result, 0, byteCount);
            return result;
        }

        private byte[] GenerateKey(string keyPhrase)
        {
            var endBlock = hashGenerator.HashBytes(keyPhrase);
            var startBlock = hashGenerator.HashBytes(new BinaryReader(new MemoryStream(endBlock)));
            var result = new byte[B];

            Buffer.BlockCopy(startBlock, 0, result, 0, HashSize);
            Buffer.BlockCopy(endBlock, 0, result, HashSize, HashSize);
            return result;
        }

        private ulong[] CreateSArray(byte[] key)
        {
            var c = B / U + (B % U == 0 ? 0 : 1);
            // created L using buffer copy(it forms L array from key (little endian order, if key length is not enough, the rest is 0)
            var l = new ulong[c];
            Buffer.BlockCopy(key, 0, l, 0, B);
            var s = new ulong[SArraySize];
            s[0] = P;
            for (int i = 1; i < SArraySize; i++)
            {
                s[i] = s[i - 1] + Q;
            }

            var t = 3 * Math.Max(SArraySize, c);
            ulong a = 0, b = 0;

            for (int k = 0, i = 0, j = 0;
                k < t;
                k++, i = (i + 1) % SArraySize, j = (j + 1) % c)
            {
                s[i] = Cls(s[i] + a + b, 3);
                a = s[i];
                l[j] = Cls(s[i] + a + b, a + b);
                b = l[j];
            }

            return s;
        }
    }
}
