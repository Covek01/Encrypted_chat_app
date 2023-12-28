using Microsoft.EntityFrameworkCore;
//using Microsoft.VisualBasic.ApplicationServices;
using CipherNew.DatabaseContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CipherNew.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace CipherNew.DataLayer
{
    public class UserManager : IUserManager
    {
        public int GetTotalAmountOfUsers()
        {
            try
            {
                var context = new Context();
                int number = context.Users.Count<User>();

                return number;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public async Task<int> GetTotalAmountOfUsersAsync()
        {
            try
            {
                var context = new Context();
                int number = await context.Users.CountAsync<User>();

                return number;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public bool InsertUser(string username)
        {
            try
            {
                var context = new Context();

                var user = new DatabaseContext.User();
                user.Username = username;

                bool userExists = this.UserExists(username);

                context.Users.Add(user);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }


        public async Task<bool> InsertUserAsync(string username)
        {
            try
            {
                var context = new Context();

                var user = new DatabaseContext.User();
                user.Username = username;

                bool userExists = this.UserExists(username);

                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public async Task RemoveUser(int idUser)
        {
            try
            {
                var context = new Context();
                var user = await context.Users
                    .Where(p => p.Id == idUser)
                    .FirstOrDefaultAsync();

                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public async Task RemoveUserAsync(string username)
        {
            try
            {
                var context = new Context();
                var user = await context.Users
                    .Where(p => p.Username == username)
                    .FirstOrDefaultAsync();

                context.Users.Remove(user);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public bool RemoveUser(string username)
        {
            try
            {
                var context = new Context();
                var user = context.Users
                    .Where(p => p.Username == username)
                    .FirstOrDefault();

                context.Users.Remove(user);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public bool UserExists(string username)
        {
            try
            {
                var context = new Context();
                var user = context.Users
                    .Where(p => p.Username == username)
                    .FirstOrDefault();

                return user != null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public int GetIdByUsername(string username)
        {
            try
            {
                var context = new Context();
                var user = context.Users
                    .Where(p => p.Username == username)
                    .FirstOrDefault();

                return user.Id;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return -1;
            }
        }

        public List<DTO.UserDTO> GetUsers()
        {
            try
            {
                var context = new Context();
                var users = context.Users.ToList();

                List<DTO.UserDTO> usersReturn = new List<UserDTO>();

                foreach (var user in users)
                {
                    usersReturn.Add(new UserDTO { ID = user.Id, Name = user.Username});
                }

                return usersReturn;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
