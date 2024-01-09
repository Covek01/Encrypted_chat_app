using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherNew.DatabaseContext
{
    internal class Message
    {
        [Key]
        public int Id { get; set; }
        public bool IsRead { get; set; }
        public bool IsFile { get; set; }
        public string Filename { get; set; }
        public string Text { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public User Sender { get; set; }
        public User Receiver { get; set; }
    }
}
