using Lab2.Core;
using System;
using System.IO;
using static Lab3.Core.BinaryOperators;
using static Lab3.Core.Rc5Constants;

namespace Lab3.Core
{
    internal static class Rc5Common
    {
        private static readonly Md5HashGenerator HashGenerator = new Md5HashGenerator();

        public static byte[] Encrypt(byte[] block, ulong[] s)
        {
            var m = new ulong[2];

            Buffer.BlockCopy(block, 0, m, 0, 2 * U);

            var a = m[0];
            var b = m[1];

            a += s[0];
            b += s[1];

            for(int i =1; i <= R; i++)
            {
                a = Cls(a ^ b, b) + s[2 * i];
                b = Cls(b ^ a, a) + s[2 * i + 1];
            }

            m[0] = a;
            m[1] = b;
            var result = new byte[2 * U];

            Buffer.BlockCopy(m, 0, result,0, 2 * U);
            return result;
        }

        public static byte[] Decrypt(byte[] block, ulong[] s)
        {
            var m = new ulong[2];

            Buffer.BlockCopy(block, 0, m, 0, 2 * U);

            var a = m[0];
            var b = m[1];


            for (int i = R; i >= 1; i--)
            {
                b = Crs(b - s[2 * i + 1], a) ^ a;
                a = Crs(a - s[2 * i], b) ^ b;
            }


            a -= s[0];
            b -= s[1];
            m[0] = a;
            m[1] = b;
            var result = new byte[2 * U];

            Buffer.BlockCopy(m, 0, result, 0, 2 * U);
            return result;
        }

        public static byte[] GenerateKey(string keyPhrase)
        {
            var endBlock = HashGenerator.HashBytes(keyPhrase);
            var startBlock = HashGenerator.HashBytes(new BinaryReader(new MemoryStream(endBlock)));
            var result = new byte[B];

            Buffer.BlockCopy(startBlock, 0, result, 0, HashSize);
            Buffer.BlockCopy(endBlock, 0, result, HashSize, HashSize);
            return result;
        }

        public static ulong[] CreateSArray(byte[] key)
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
