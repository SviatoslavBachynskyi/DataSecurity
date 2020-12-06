using Lab1.Core;
using System;
using System.IO;
using System.Linq;
using static Lab3.Core.Rc5Common;
using static Lab3.Core.Rc5Constants;

namespace Lab3.Core
{
    public class Rc5CbcPad
    {
        public void Encrypt(BinaryReader input, string keyPhrase, BinaryWriter output)
        {
            var enumerable = new Rc5BinaryEnumerable(input);
            var vector = GenerateVector();
            var key = GenerateKey(keyPhrase);
            var s = CreateSArray(key);
            for (int i = 0; i < B; i++)
            {
                key[i] = 0;
            }

            var vectorEncrypted = Rc5Common.Encrypt(vector, s);
            output.Write(vectorEncrypted);

            foreach (var block in enumerable)
            {
                var forEncryption = block.Zip(vector, (fst, snd) => (byte)(fst ^ snd)).ToArray();
                var encrypted = Rc5Common.Encrypt(forEncryption, s);
                output.Write(encrypted);
                vector = encrypted;
            }           

            for (int i = 0; i < SArraySize; i++)
            {
                s[i] = 0;
            }
        }

        public void Decrypt(BinaryReader input, string keyPhrase, BinaryWriter output)
        {
            var block = new byte[2 * U];
            var key = GenerateKey(keyPhrase);
            var s = CreateSArray(key);
            for (int i = 0; i < B; i++)
            {
                key[i] = 0;
            }

            input.Read(block);
            var vector = Rc5Common.Decrypt(block, s);

            while (input.Read(block) > 0)
            {
                var partlyDecrypted = Rc5Common.Decrypt(block, s);
                var decrypted = partlyDecrypted.Zip(vector, (fst, snd) => (byte)(fst ^ snd)).ToArray();
                output.Write(decrypted);
                vector = block;
            }

            for (int i = 0; i < SArraySize; i++)
            {
                s[i] = 0;
            }
        }

        private byte[] GenerateVector()
        {
            var byteCount = 2 * U;
            var constants = new LcConstants((uint)Environment.TickCount, 16807, 28657, 2147483648);
            var stringWriter = new StringWriter();
            var vector = LcPseudoRandomGenerator.Generate(constants, null, stringWriter, (uint)byteCount / 4);
            var stringNumbers = stringWriter.ToString();
            var numbers = stringNumbers.Split(", ").Select(UInt32.Parse).ToArray();
            var result = new byte[2 * U];

            Buffer.BlockCopy(numbers, 0, result, 0, byteCount);
            return result;
        }
    }
}
