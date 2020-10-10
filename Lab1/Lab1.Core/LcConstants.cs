namespace Lab1.Core
{
    public class LcConstants
    {
        public LcConstants(uint x0, uint a, uint c, uint m)
        {
            X0 = x0;
            A = a;
            C = c;
            M = m;
        }

        public uint X0 { get; private set; }

        public uint A { get; private set; }
        
        public uint C { get; private set; }
        
        public uint M { get; private set; }
    }
}
