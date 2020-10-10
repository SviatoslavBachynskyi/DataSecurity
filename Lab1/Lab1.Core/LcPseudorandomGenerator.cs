using System.Collections.Generic;
using System.IO;

namespace Lab1.Core
{
    public class LcPseudorandomGenerator
    {
        public StringWriter StringWriter { get; set; }

        public StreamWriter StreamWriter { get; set; }

        public int Generator(int x0, int a, int c, int m, int count = 0)
        {
            var writers = new List<TextWriter>();

            if (StringWriter != null && count != 0)
            {
                writers.Add(StringWriter);
            }
            if (StreamWriter != null)
            {
                writers.Add(StreamWriter);
            }

            if (writers.Count == 0)
            {
                throw new IOException("no writers were provided");
            }

            int x = x0, i = 0, period = 0;

            while (writers.Count != 0)
            {
                foreach (var writer in writers)
                {
                    writer.Write(i == 0 ? $"{x}" : $", {x}");
                }

                x = (a * x + c) % m;
                i++;

                if (x == x0 && writers.Contains(StreamWriter))
                {
                    period = i;
                    writers.Remove(StreamWriter);
                }

                if (i == count && writers.Contains(StringWriter))
                {
                    writers.Remove(StringWriter);
                }
            }

            return period;
        }
    }
}
