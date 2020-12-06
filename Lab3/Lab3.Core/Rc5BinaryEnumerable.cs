using System.Collections;
using System.Collections.Generic;
using System.IO;
using static Lab3.Core.Rc5Constants;

namespace Lab3.Core
{
    internal class Rc5BinaryEnumerable : IEnumerable<byte[]>
    {
        private readonly BinaryReader _reader;
        private readonly byte[] _buffer = new byte[2 * U];

        public Rc5BinaryEnumerable(BinaryReader reader)
        {
            _reader = reader;
        }

        public IEnumerator<byte[]> GetEnumerator()
        {
            int bytesRead = _reader.Read(_buffer);
            while (_reader.BaseStream.Length != _reader.BaseStream.Position)
            {
                yield return _buffer;
                bytesRead = _reader.Read(_buffer);
            }

            var addFullBlock = bytesRead == 2 * U;

            if (addFullBlock)
            {
                yield return _buffer;
                bytesRead = 0;
            }

            var appendLength = (byte)(2 * U - bytesRead);

            for (; bytesRead < 2 * U; bytesRead++)
            {
                _buffer[bytesRead] = appendLength;
            }

            yield return _buffer;

        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
