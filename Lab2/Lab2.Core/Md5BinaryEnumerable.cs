using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

namespace Lab2.Core
{
    internal class Md5BinaryEnumerable : IEnumerable<byte[]>
    {
        private const int BytesInBlock = 64;
        private const int BytesBeforeLength = 56;

        private readonly BinaryReader _reader;
        private readonly byte[] _buffer = new byte[BytesInBlock];

        public Md5BinaryEnumerable(BinaryReader reader)
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

            byte firstByteToAdd = 0x80;
            byte otherBytesToAdd = 0x00;
            bool firstByteWasAdded = false;

            var addFullBlock = bytesRead >= BytesBeforeLength;

            do
            {
                for (; bytesRead < (addFullBlock ? BytesInBlock : BytesBeforeLength); bytesRead++)
                {
                    if (firstByteWasAdded)
                    {
                        _buffer[bytesRead] = otherBytesToAdd;
                    }
                    else
                    {
                        _buffer[bytesRead] = firstByteToAdd;
                        firstByteWasAdded = true;
                    }
                }

                if (addFullBlock)
                {
                    yield return _buffer;
                    addFullBlock = false;
                    bytesRead = 0;
                }
                else
                {
                    Buffer.BlockCopy(new[] { (ulong)_reader.BaseStream.Length * 8 },0, _buffer, BytesBeforeLength, BytesInBlock - BytesBeforeLength);
                    yield return _buffer;
                    yield break;
                }

            } while (true);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
