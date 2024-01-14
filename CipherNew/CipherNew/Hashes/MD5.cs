using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Runtime;
using System.Text.Unicode;

namespace CipherNew.Hashes
{
    public class MD5
    {
        public static string Hash(string message)
        {
            uint[] s = new uint[64];
            uint[] k = new uint[64];
            int i;

            int border = 64 / 4;
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            //initialize s
            for (i = 0; i <= border - 1; i += 4)
            {
                s[i] = 7;
                s[i + 1] = 12;
                s[i + 2] = 17;
                s[i + 3] = 22;
            }
            for (i = border; i <= 2 * border - 1; i += 4)
            {
                s[i] = 5;
                s[i + 1] = 9;
                s[i + 2] = 14;
                s[i + 3] = 20;
            }
            for (i = 2 * border; i <= 3 * border - 1; i += 4)
            {
                s[i] = 4;
                s[i + 1] = 11;
                s[i + 2] = 16;
                s[i + 3] = 23;
            }
            for (i = 3 * border; i <= 4 * border - 1; i += 4)
            {
                s[i] = 6;
                s[i + 1] = 10;
                s[i + 2] = 15;
                s[i + 3] = 21;
            }

            for (i = 0; i <= 63; i++)
            {
                k[i] = (uint)Math.Floor(Math.Pow(2, 32) * Math.Abs(Math.Sin(i + 1)));
            }


            //creating appended message with 1 and 0s
            int newLengthOfMessageInBytes = messageBytes.Length + 1;
            int deltaL = (56 - (newLengthOfMessageInBytes % 64)) % 64;

            newLengthOfMessageInBytes += deltaL + sizeof(ulong);
            byte[] destMessageBytes = new byte[newLengthOfMessageInBytes];

            Buffer.BlockCopy(messageBytes, 0, destMessageBytes, 0, messageBytes.Length); //Copying original message
            destMessageBytes[messageBytes.Length] = 0x80;

            for (int byteIndex = messageBytes.Length + 1; byteIndex < newLengthOfMessageInBytes - sizeof(ulong); byteIndex++)
            {
                destMessageBytes[byteIndex] = 0x00;
            } //initialize everythingt to end with 0x00

            ulong originalLengthInBits = (ulong)messageBytes.Length * 8;
            byte[] originalLengthInBitsBytes = BitConverter.GetBytes(originalLengthInBits);

            originalLengthInBitsBytes.CopyTo(destMessageBytes, newLengthOfMessageInBytes - sizeof(ulong));

            uint a0 = 0x67452301;
            uint b0 = 0xefcdab89;
            uint c0 = 0x98badcfe;
            uint d0 = 0x10325476;


            //for every 512b chunk
            for (int index = 0; index <= destMessageBytes.Length - 1; index += 64 * sizeof(byte))
            {
                uint[] M = new uint[16];
                for (int j = 0; j < 16; j++)
                {
                    M[j] = BitConverter.ToUInt32(destMessageBytes, index + j * sizeof(uint));
                }

                uint A = a0;
                uint B = b0;
                uint C = c0;
                uint D = d0;


                for (uint j = 0; j < 64; j++)
                {
                    uint F, g;
                    if (j <= 15 && j >= 0)
                    {
                        F = (B & C) | ((~B) & D);
                        g = j;
                    }
                    else if (j <= 31)
                    {
                        F = (D & B) | ((~D) & C);
                        g = (5 * j + 1) % 16;
                    }
                    else if (i <= 47)
                    {
                        F = B ^ C ^ D;
                        g = (3 * j + 5) % 16;
                    }
                    else
                    {
                        F = C ^ (B | (~D));
                        g = (7 * j) % 16;
                    }

                    F = F + A + k[j] + M[g];
                    A = D;
                    D = C;
                    C = B;
                    B = B + LeftRotate(F, (int)s[j]);
                }

                a0 += A;
                b0 += B;
                c0 += C;
                d0 += D;
            }

            byte[] outputBytes = new byte[4 * sizeof(uint)];

            Buffer.BlockCopy(BitConverter.GetBytes(a0), 0, outputBytes, 0 * sizeof(uint), sizeof(uint));
            Buffer.BlockCopy(BitConverter.GetBytes(b0), 0, outputBytes, 1 * sizeof(uint), sizeof(uint));
            Buffer.BlockCopy(BitConverter.GetBytes(c0), 0, outputBytes, 2 * sizeof(uint), sizeof(uint));
            Buffer.BlockCopy(BitConverter.GetBytes(d0), 0, outputBytes, 3 * sizeof(uint), sizeof(uint));

            //reverse bytes if storing is in little endian
            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(outputBytes);
            }
            // uint[] output = new uint[4]{
            //     a0, b0, c0, d0
            // };

            StringBuilder outputBuilder = new StringBuilder();
            foreach (byte bajt in outputBytes)
            {
                outputBuilder.Append(bajt.ToString("X2"));   //converts to hexa value
            }

            return outputBuilder.ToString();

        }

        private static uint LeftRotate(uint val, int shift)
        {
            return (val << shift) | (val >> (32 - shift));
        }

        // public static bool CompareTwoHashes(uint[] hash1, uint[] hash2)
        // {
        //     if (hash1.Length != hash2.Length)
        //     {
        //         return false;
        //     }

        //     bool flag = true;
        //     for (int i = 0; i <= hash1.Length - 1; i++)
        //     {
        //         flag = flag && hash1[i] == hash2[i];
        //     }
        //     return true;
        // }
    }
}
