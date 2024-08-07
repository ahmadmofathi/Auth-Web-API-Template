using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWeb.BL
{
    public interface IUserManager
    {
        IEnumerable<UserDTO> GetAllUsers();
        UserDTO? GetUserById(string id);
        string AddUser(UserAddDTO user);
        bool UpdateUser(UserDTO user);
        bool DeleteUser(string id);
    }
}
