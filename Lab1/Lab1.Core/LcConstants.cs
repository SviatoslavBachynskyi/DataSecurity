namespace Lab1.Core
{
    public class LcConstants
    {
        public LcConstants(int x0, int a, int c, int m)
        {
            X0 = x0;
            A = a;
            C = c;
            M = m;
        }

        public int X0 { get; private set; }

        public int A { get; private set; }
        
        public int C { get; private set; }
        
        public int M { get; private set; }
    }
}
