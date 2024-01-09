using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherNew.DTO
{
    public class CachedFile
    {
        public string Name { get; set; }
        public string Text { get; set; }

        public CachedFile(string name, string text)
        {
            Name = name;
            Text = text;
        }
    }
}
