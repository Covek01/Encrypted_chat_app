using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherNew.DTO
{
    public class MessageWithSender
    {
        public string Sender { get; set; }
        public string Message { get; set; }
        public bool isFile { get; set; }
        public string Filename { get; set; }
    }

}
