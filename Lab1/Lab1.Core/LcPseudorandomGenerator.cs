using System.Collections.Generic;
using System.IO;

namespace Lab1.Core
{
    public static class LcPseudoRandomGenerator
    {
        public static uint Generate(LcConstants constants, StreamWriter streamWriter = null, StringWriter stringWriter = null, uint count = 0)
        {
            var writers = new List<TextWriter>();
            ulong x0 = constants.X0;
            ulong a = constants.A;
            ulong c = constants.C;
            ulong m = constants.M;

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

            ulong x = constants.X0;
            uint i = 0, period = 0;

            while (writers.Count != 0)
            {
                foreach (var writer in writers)
                {
                    writer.Write(i == 0 ? $"{x}" : $", {x}");
                }

                x = ((a  % m * x % m) % m + c % m) % m;
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
