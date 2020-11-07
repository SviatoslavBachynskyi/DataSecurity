using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Lab2.Core
{
    public class Md5HashGenerator
    {
        public string Hash(BinaryReader binaryReader)
        {
            throw new NotImplementedException();
        }

        public string Hash(string textInput)
        {
            var binaryReader = new BinaryReader(
                new MemoryStream(Encoding.UTF8.GetBytes(textInput ?? ""))
                );

            return Hash(binaryReader);
        }
    }
}
