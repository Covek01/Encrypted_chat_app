using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CipherNew.DataLayer
{
    public interface IUserManager
    {
        int GetTotalAmountOfUsers();
        List<DTO.UserDTO> GetUsers();
        Task<int> GetTotalAmountOfUsersAsync();
        Task<bool> InsertUserAsync(string username);
        bool InsertUser(string username);
        Task RemoveUser(int idUser);
        bool RemoveUser(string username);
        Task RemoveUserAsync(string username);
        bool UserExists(string username);
        int GetIdByUsername(string username);
    }
}
