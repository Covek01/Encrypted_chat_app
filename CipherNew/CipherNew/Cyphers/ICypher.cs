using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherNew.Cyphers
{
    interface ICypher
    {
        string Encrypt(string textInserted);
        string Decrypt(string encryptedText);

    }
}
