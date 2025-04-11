using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWeb.DAL
{
    public interface IUserRepo
    {
        Task<IEnumerable<User>> GetAllUsers();
        Task<User?> GetUserById(string userId);
        Task<string> Add(User user);
        Task<bool> Delete(User user);
        Task<bool> Update(User user);
        Task<int> SaveChanges();
    }
}
