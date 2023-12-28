using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text;
using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using CipherNew.Cyphers;
using System.Configuration;


namespace CypherNew.Cyphers
{
    public class XXTeaWithCBCCypher : ICypher
    {

        public XXTeaWithCBCCypher()
        {
        }


        public void XXTea(uint[] v, int n)
        {
            uint[] key = XXTeaWithCBCCypher.GetKeyXXTea();
            uint z, y, sum = 0, e, rounds;
            const uint delta = 0x9e3779b9;
            uint p;

            if (n > 1)
            {          //encrypting
                z = v[n - 1];
                rounds = (uint)(6 + 52 / n);
                sum = 0;
                while (rounds > 0)
                {
                    sum += delta;
                    e = (sum >> 2) & 3;
                    for (p = 0; p < n - 1; p++)
                    {
                        y = v[p + 1];
                        z = v[p] += mx(y, z, sum, e, key, p);
                    }
                    y = v[0];
                    z = v[n - 1] += mx(y, z, sum, e, key, p);

                    rounds--;
                }

            }
            else if (n < -1)
            {  //decrpyting
                n = -n;

                rounds = (uint)(6 + 52 / n);
                sum = rounds * delta;
                y = v[0];
                while (rounds > 0)
                {
                    e = (sum >> 2) & 3;
                    for (p = (uint)n - 1; p > 0; p--)
                    {
                        z = v[p - 1];
                        y = v[p] -= mx(y, z, sum, e, key, p);
                    }
                    z = v[n - 1];
                    y = v[0] -= mx(y, z, sum, e, key, p);
                    sum -= delta;

                    rounds--;
                }

            }

        }

        public uint mx(uint y, uint z, uint sum, uint e, uint[] key, uint p)
        {
            return ((z >> 5 ^ y << 2) + (y >> 3 ^ z << 4)) ^ ((sum ^ y) + (key[(int)((p & 3) ^ e)] ^ z));
        }


        public string Encrypt(string textInserted)
        {

            while (textInserted.Length % (2 * sizeof(uint)) != 0)
            {
                textInserted += '\0';
            }

            byte[] inputBytes = Encoding.UTF8.GetBytes(textInserted);
            int blockSize = 2 * sizeof(uint);

            int numberOfBlocks = (int)Math.Ceiling((double)inputBytes.Length / blockSize);

            byte[] block = new byte[2 * sizeof(uint)];
            uint[] blockCastedToUint = new uint[block.Length / sizeof(uint)];

            Array.Copy(inputBytes, 0, block, 0, Math.Min(blockSize, inputBytes.Length));
            uint[] v = new uint[block.Length / sizeof(uint)];
            Buffer.BlockCopy(block, 0 * blockSize, v, 0, block.Length - 0 * blockSize);

            byte[] encryptedDataBytes = new byte[inputBytes.Length];

            int i, j;


            //SECONDARY KEY FOR CBC
            uint[] initializationVectorCBC = XXTeaWithCBCCypher.GetInitializationVectorCBC();


           
            for (i = 0; i <= numberOfBlocks - 1; i++)
            {
                Buffer.BlockCopy(inputBytes, blockSize * i, v, 0, block.Length);

                for (j = 0; j < initializationVectorCBC.Length; j++)
                {
                    v[j] ^= initializationVectorCBC[j];   //XOR OPERATION ON INITIALIZATION VECTOR AND INPUT TEXT
                }

                XXTea(v, v.Length);
                Buffer.BlockCopy(v, 0, encryptedDataBytes, blockSize * i, block.Length);
                //initialization of entry for next block
                Buffer.BlockCopy(v, 0, initializationVectorCBC, 0, v.Length * sizeof(uint));

            }

            string encryptedString = Convert.ToBase64String(encryptedDataBytes);

            return encryptedString;
        }




        public string Decrypt(string textInserted)
        {
            byte[] inputBytes = Convert.FromBase64String(textInserted);
            int blockSize = 2 * sizeof(uint);

            int numberOfBlocks = (int)Math.Ceiling((double)inputBytes.Length / blockSize);

            byte[] block = new byte[2 * sizeof(uint)];
            uint[] blockCastedToUint = new uint[block.Length / sizeof(uint)];

            Array.Copy(inputBytes, 0 * blockSize, block, 0, Math.Min(blockSize, inputBytes.Length - 0 * blockSize));
            uint[] v = new uint[block.Length / sizeof(uint)];
            Buffer.BlockCopy(block, 0 * blockSize, v, 0, block.Length - 0 * blockSize);

            byte[] decryptedDataBytes = new byte[inputBytes.Length];

            int i, j;


            //SECONDARY KEY FOR CBC
            uint[] initializationVectorCBC = XXTeaWithCBCCypher.GetInitializationVectorCBC();


            

            uint[] vCopy = new uint[v.Length];  //kopija od v sa kojim se radi zbog xora
            for (i = 0; i <= numberOfBlocks - 1; i++)
            {


                Buffer.BlockCopy(inputBytes, blockSize * i, v, 0, block.Length - 0 * blockSize);
                Buffer.BlockCopy(inputBytes, blockSize * i, vCopy, 0, block.Length - 0 * blockSize);

                XXTea(v, -v.Length);

                for (j = 0; j < initializationVectorCBC.Length; j++)
                {
                    v[j] ^= initializationVectorCBC[j];   //XOR OPERATION ON INITIALIZATION VECTOR AND INPUT TEXT
                }

                Buffer.BlockCopy(v, 0, decryptedDataBytes, blockSize * i, block.Length);
                //initialization of entry for next block
                Buffer.BlockCopy(vCopy, 0, initializationVectorCBC, 0, v.Length * sizeof(uint));

            }

            string encryptedString = Encoding.UTF8.GetString(decryptedDataBytes).TrimEnd('\0');

            return encryptedString;
        }

        private static uint[] GetKeyXXTea()
        {
            string[] keyStrings = ConfigurationManager.AppSettings["key_xxtea"].Split(',');
            uint[] key = new uint[keyStrings.Length];
            int counter = 0;
            int hexa = 16;
            foreach (string chunk in keyStrings)
            {
                key[counter++] = UInt32.Parse(chunk);
            }

            return key;
        }

        private static uint[] GetInitializationVectorCBC()
        {
            string[] keyStrings = ConfigurationManager.AppSettings["key_cbc"].Split(',');
            uint[] key = new uint[keyStrings.Length];
            int counter = 0;
            foreach (string chunk in keyStrings)
            {
                key[counter++] = UInt32.Parse(chunk);
            }

            return key;
        }
    }
}