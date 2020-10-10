using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab1.Core
{
    public static class LcPseudoRandomGenerator
    {
        public static uint Generate(LcConstants constants, StreamWriter streamWriter = null, StringWriter stringWriter = null, uint count = 0)
        {
            const ulong lastAcceptedValue = 2147483648;

            var writers = new List<TextWriter>();
            ulong x0 = constants.X0;
            ulong a = constants.A;
            ulong c = constants.C;
            ulong m = constants.M;

            if (m > lastAcceptedValue)
            {
                throw new ArgumentException($"value '{m}' is higher than last accepted value for m - '{lastAcceptedValue}'");
            }

            var lastPossibleNumberAppeared = false;
            var numberAppearanceArray = new BitArray((m == lastAcceptedValue) ? (int)(m - 1) : (int)m);

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
                if (writers.Contains(streamWriter))
                {
                    var wasAdded = x == lastAcceptedValue - 1 ? lastPossibleNumberAppeared : numberAppearanceArray.Get((int)x);
                    if (wasAdded)
                    {
                        period = i;
                        writers.Remove(streamWriter);
                    }
                    else
                    {
                        if (x == lastAcceptedValue)
                            lastPossibleNumberAppeared = true;
                        else
                            numberAppearanceArray.Set((int)x, true);
                    }
                }
                if (i == count && writers.Contains(stringWriter))
                {
                    writers.Remove(stringWriter);
                }

                foreach (var writer in writers)
                {
                    writer.Write(i == 0 ? $"{x}" : $", {x}");
                }

                x = ((a % m * x % m) % m + c % m) % m;
                i++;
            }

            return period;
        }
    }
}
