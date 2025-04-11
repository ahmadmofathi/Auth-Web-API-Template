using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateWeb.DAL;

namespace TemplateWeb.BL
{
    public interface IUserManager
    {
        Task<IEnumerable<UserDTO>?> GetAllUsers();
        Task<UserDTO?> GetUserById(string id);
        Task<UserAddDTO> AddUser(UserAddDTO user);
        Task<bool> UpdateUser(UserDTO user);
        Task<bool> DeleteUser(string id);
        Task<IEnumerable<UserLoginHistory>> GetLoginHistoryByUserId(string userId);
        Task LogUserLogin(string userId, string? username, string ipAddress, string userAgent, bool isSuccess);

    }
}
