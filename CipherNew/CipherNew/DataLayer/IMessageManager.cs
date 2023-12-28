using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherNew.DataLayer
{
    public interface IMessageManager
    {
        bool InsertMessage(string text, string senderUsername);
        Task RemoveMessage(int idMessage);
        List<string>? ReadAllReceivedMessages(int id);
        bool RemoveMessagesBySender(int idSender);
        bool RemoveMessages();
    }
}
