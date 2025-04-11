using Microsoft.EntityFrameworkCore;
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
        public async Task<string> Add(User user)
        {
            if (user.Id == null)
            {
                return "Not Found";
            }
            await _context.Set<User>().AddAsync(user);

            return user.Id;
        }

        public async Task<bool> Delete(User user)
        {
            _context.Set<User>().Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _context.Set<User>().ToListAsync();
        }

        public async Task<User?> GetUserById(string userId)
        {
            return await _context.Set<User>().FindAsync(userId);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
