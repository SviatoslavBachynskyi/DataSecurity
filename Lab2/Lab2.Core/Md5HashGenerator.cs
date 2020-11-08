using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Lab2.Core
{
    public class Md5HashGenerator
    {
        private const int IterationsInCycle = 16;
        private const int NumberOfCycles = 4;
        private const int ResultLength = 16;

        public string Hash(BinaryReader binaryReader)
        {
            var md5Enumerable = new Md5BinaryEnumerable(binaryReader);
            uint a = 0x67452301, b = 0xEFCDAB89, c = 0x98BADCFE, d = 0x10325476;
            var uintBlock = new uint[IterationsInCycle];

            foreach (var block in md5Enumerable)
            {
                var a0 = a;
                var b0 = b;
                var c0 = c;
                var d0 = d;
                Buffer.BlockCopy(block, 0, uintBlock, 0, IterationsInCycle * NumberOfCycles);
                for (int ci = 0; ci < NumberOfCycles; ci++)
                {
                    var cycle = Cycles[ci];
                    for (int i = 0; i < IterationsInCycle; i++)
                    {
                        uint tmp = b0 + Cls(a0 + cycle.F(b0, c0, d0) + uintBlock[cycle.TossI(i)] + cycle.T[i], cycle.S[i]);
                        a0 = d0;
                        d0 = c0;
                        c0 = b0;
                        b0 = tmp;
                    }
                }

                a += a0;
                b += b0;
                c += c0;
                d += d0;
            }

            var resultBytes = new byte[ResultLength];
            Buffer.BlockCopy(new []{a, b, c, d}, 0, resultBytes, 0, ResultLength);

            return String.Join("",resultBytes.Select(elem =>  $"{elem:X2}"));
        }

        public string Hash(string textInput)
        {
            var binaryReader = new BinaryReader(
                new MemoryStream(Encoding.ASCII.GetBytes(textInput ?? ""))
            );

            return Hash(binaryReader);
        }

        private uint Cls(uint val, int shift)
        {
            return val << shift | val >> (32 - shift);
        }

        private static readonly Cycle[] Cycles =
        {
            new Cycle()
            {
                T = new uint[IterationsInCycle]
                {
                    0xd76aa478, 0xe8c7b756, 0x242070db, 0xc1bdceee, 0xf57c0faf, 0x4787c62a, 0xa8304613, 0xfd469501,
                    0x698098d8, 0x8b44f7af, 0xffff5bb1, 0x895cd7be, 0x6b901122, 0xfd987193, 0xa679438e, 0x49b40821,
                },
                S = new int[IterationsInCycle]
                {
                    7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22, 7, 12, 17, 22,
                },
                F = (b, c, d) => (b & c) | (~b & d),
                TossI = (i) => i,
            },
            new Cycle()
            {
                T = new uint[IterationsInCycle]
                {
                    0xf61e2562, 0xc040b340, 0x265e5a51, 0xe9b6c7aa, 0xd62f105d, 0x02441453, 0xd8a1e681, 0xe7d3fbc8,
                    0x21e1cde6, 0xc33707d6, 0xf4d50d87, 0x455a14ed, 0xa9e3e905, 0xfcefa3f8, 0x676f02d9, 0x8d2a4c8a,
                },
                S = new int[IterationsInCycle]
                {
                    5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20, 5, 9, 14, 20,
                },
                F = (b, c, d) => (b & d) | (c & ~d),
                TossI = (i) => (1 + 5 * i) % 16,
            },
            new Cycle()
            {
                T = new uint[IterationsInCycle]
                {
                    0xfffa3942, 0x8771f681, 0x6d9d6122, 0xfde5380c, 0xa4beea44, 0x4bdecfa9, 0xf6bb4b60, 0xbebfbc70,
                    0x289b7ec6, 0xeaa127fa, 0xd4ef3085, 0x04881d05, 0xd9d4d039, 0xe6db99e5, 0x1fa27cf8, 0xc4ac5665,
                },
                S = new int[IterationsInCycle]
                {
                    4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23, 4, 11, 16, 23,
                },
                F = (b, c, d) => b ^ c ^ d,
                TossI = (i) => (5 + 3 * i) % 16,
            },
            new Cycle()
            {
                T = new uint[IterationsInCycle]
                {
                    0xf4292244, 0x432aff97, 0xab9423a7, 0xfc93a039, 0x655b59c3, 0x8f0ccc92, 0xffeff47d, 0x85845dd1,
                    0x6fa87e4f, 0xfe2ce6e0, 0xa3014314, 0x4e0811a1, 0xf7537e82, 0xbd3af235, 0x2ad7d2bb, 0xeb86d391,

                },
                S = new int[IterationsInCycle]
                {
                    6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21, 6, 10, 15, 21,
                },
                F = (b, c, d) => c ^ (b | ~d),
                TossI = (i) => (7 * i) % 16,
            },
        };

        private class Cycle
        {
            public uint[] T { get; set; }

            public int[] S { get; set; }

            public Func<uint, uint, uint, uint> F { get; set; }

            public Func<int, int> TossI { get; set; }
        }
    }
}
