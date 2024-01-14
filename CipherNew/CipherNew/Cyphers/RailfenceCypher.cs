using System.Text;
using System;
using System.Globalization;
using System.Configuration;

namespace CipherNew.Cyphers
{
    public class RailFenceCypher : ICypher
    {
        private readonly bool _isCaseSensitive;
        public RailFenceCypher(bool isCaseSensitive)
        {
            _isCaseSensitive = isCaseSensitive;
        }
        public string Encrypt(string textInserted)
        {
            uint key = UInt32.Parse(ConfigurationManager.AppSettings["key"]);

            string text;
            if (!_isCaseSensitive)
            {
                text = textInserted.ToLower();
            }
            else 
            {
                text = textInserted;
            }


            char[,] matrix = new char[key, text.Length];

            int i, countRow = 0, j;
            bool dirUp = true;

            for (i = 0; i <= text.Length - 1; i++)
            {
                matrix[countRow, i] = text[i];
                countRow = (dirUp) ? ++countRow : --countRow;
                dirUp = (countRow == key - 1 || countRow == 0) ? !dirUp : dirUp;

            }

            StringBuilder cipheredString = new StringBuilder();

            for (i = 0; i <= key - 1; i++)
            {
                for (j = 0; j <= text.Length - 1; j++)
                {
                    if (matrix[i, j] != '\0')
                    {
                        cipheredString.Append(matrix[i, j]);
                    }
                }
            }

            return cipheredString.ToString();
        }

        public string Decrypt(string encryptedText)
        {
            uint key = UInt32.Parse(ConfigurationManager.AppSettings["key"]);

            char[,] matrix = new char[key, encryptedText.Length];

            int i, countRow = 0, j;
            bool dirUp = true;

            //mark places
            for (i = 0; i <= encryptedText.Length - 1; i++)
            {
                matrix[countRow, i] = '*';
                countRow = (dirUp) ? ++countRow : --countRow;
                dirUp = (countRow == key - 1 || countRow == 0) ? !dirUp : dirUp;
            }

            //place letters on places marked with *
            int encryptedTextCounter = 0;
            for (i = 0; i <= key - 1; i++)
            {
                for (j = 0; j <= encryptedText.Length - 1; j++)
                {
                    if (matrix[i, j] == '*')
                    {
                        matrix[i, j] = encryptedText[encryptedTextCounter++];
                    }
                }
            }


            StringBuilder originalText = new StringBuilder();
            dirUp = true;
            countRow = 0;
            //iterate zig-zag to get original string
            for (i = 0; i <= encryptedText.Length - 1; i++)
            {
                originalText.Append(matrix[countRow, i]);
                countRow = (dirUp) ? ++countRow : --countRow;
                dirUp = (countRow == key - 1 || countRow == 0) ? !dirUp : dirUp;
            }


            return originalText.ToString();
        }
    }
}
