namespace Lab3.Core
{
    internal static class BinaryOperators
    {
        public static ulong Cls(ulong val, ulong shift)
        {
            var simpleShift = (int) shift % 64;
            return val << simpleShift | val >> (64 - simpleShift);
        }

        public static ulong Crs(ulong val, ulong shift)
        {
            var simpleShift = (int)shift % 64;
            return val >> simpleShift | val << (64 - simpleShift);
        }
    }
}
