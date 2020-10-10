using System.Collections.Generic;
using System.IO;

namespace Lab1.Core
{
    public static class LcPseudoRandomGenerator
    {
        public static int Generator(LcConstants constants, StreamWriter streamWriter = null, StringWriter stringWriter = null, int count = 0)
        {
            var writers = new List<TextWriter>();

            if (stringWriter != null && count != 0)
            {
                writers.Add(stringWriter);
            }
            if (streamWriter != null)
            {
                writers.Add(streamWriter);
            }

            if (writers.Count == 0)
            {
                throw new IOException("no writers were provided");
            }

            int x = constants.X0, i = 0, period = 0;

            while (writers.Count != 0)
            {
                foreach (var writer in writers)
                {
                    writer.Write(i == 0 ? $"{x}" : $", {x}");
                }

                x = (constants.A * x + constants.C) % constants.M;
                i++;

                if (x == constants.X0 && writers.Contains(streamWriter))
                {
                    period = i;
                    writers.Remove(streamWriter);
                }

                if (i == count && writers.Contains(stringWriter))
                {
                    writers.Remove(stringWriter);
                }
            }

            return period;
        }
    }
}
