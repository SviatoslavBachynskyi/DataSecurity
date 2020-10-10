using System.IO;

namespace Lab1.Test
{
    public class StreamWriterMock : StreamWriter
    {
        public StreamWriterMock(Stream stream) : base(stream)
        {
        }

        public string LastWriteCall { get; set; }

        public override void Write(string? value)
        {
            LastWriteCall = value;
        }
    }
}
