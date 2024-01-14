using CipherNew.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherNew.DataLayer
{
    public class MessageManager : IMessageManager
    {
        public bool InsertMessage(string text, string senderUsername)
        {
            try
            {
                var context = new Context();
                var message = new DatabaseContext.Message();

                message.Text = text;
                message.IsRead = false;
                message.IsFile = false;
                message.Filename = String.Empty;
                message.Hash = String.Empty;
                message.Sender = context.Users
                    .Where(p => p.Username == senderUsername)
                    .FirstOrDefault();
                message.Receiver = context.Users
                    .Where(p => p.Username != senderUsername)
                    .FirstOrDefault();

                context.Messages.Add(message);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool InsertFile(string filename, string text, string senderUsername, string hash)
        {
            try
            {
                var context = new Context();
                var message = new DatabaseContext.Message();

                message.Text = text;
                message.IsRead = false;
                message.IsFile = true;
                message.Filename = filename;
                message.Hash = hash;
                message.Sender = context.Users
                    .Where(p => p.Username == senderUsername)
                    .FirstOrDefault();
                message.Receiver = context.Users
                    .Where(p => p.Username != senderUsername)
                    .FirstOrDefault();

                context.Messages.Add(message);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task RemoveMessage(int idMessage)
        {
            try
            {
                var context = new Context();
                var message = new DatabaseContext.Message();


                message.IsRead = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public List<string>? ReadAllReceivedMessages(int id)
        {
            try
            {
                var context = new Context();

                var messageObjects = context.Messages
                    .Where(p => p.ReceiverId == id && p.IsRead == false)
                    .ToList();

                List<string> messages = messageObjects
                    .Select(p => p.Text)
                    .ToList();

                messageObjects.ForEach(p => p.IsRead = true);

                context.Update(messageObjects);
                context.SaveChanges();

                return messages;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }

        public bool RemoveMessagesBySender(int idSender)
        {
            try
            {
                var context = new Context();

                var messages = context.Messages
                    .Where(p => p.SenderId == idSender)
                    .ToList();

                context.RemoveRange(messages);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool RemoveMessages()
        {
            try
            {
                var context = new Context();

                var messages = context.Messages
                    .ToList();

                context.RemoveRange(messages);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }
    }
}
