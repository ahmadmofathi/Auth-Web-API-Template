using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWeb.DAL
{
    public class UserRepo : IUserRepo
    {
        private readonly AppDbContext _context;

        public UserRepo(AppDbContext context)
        {
            _context = context;
        }
        public string Add(User user)
        {
            if (user.Id == null)
            {
                return "Not Found";
            }
            _context.Set<User>().Add(user);

            return user.Id;
        }

        public bool Delete(User user)
        {
            _context.Set<User>().Remove(user);
            return true;
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Set<User>().ToList();
        }

        public User? GetUserById(string userId)
        {
            return _context.Set<User>().Find(userId);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public bool Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
