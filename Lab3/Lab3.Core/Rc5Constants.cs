namespace Lab3.Core
{
    internal static class Rc5Constants
    {
        public const int W = 64;
        public static readonly int U = W / 8;
        public const int R = 16;
        public static readonly int SArraySize = 2 * R + 2;
        public const int B = 32;
        public const int HashSize = 16;
        public const ulong P = 0xB7E151628AED2A6B;
        public const ulong Q = 0x9E3779B97F4A7C15;
    }
}
