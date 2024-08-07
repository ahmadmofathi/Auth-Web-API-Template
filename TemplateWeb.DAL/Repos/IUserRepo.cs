using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWeb.DAL
{
    public interface IUserRepo
    {
        IEnumerable<User> GetAllUsers();
        User? GetUserById(string userId);
        string Add(User user);
        bool Delete(User user);
        bool Update(User user);
        int SaveChanges();
    }
}
