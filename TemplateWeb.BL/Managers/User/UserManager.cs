using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using TemplateWeb.DAL;

namespace TemplateWeb.BL
{
    public class UserManager : IUserManager
    {
        private readonly IUserRepo _userRepo;
        private readonly ILogUserLogin _logUserLogin;

        public UserManager(IUserRepo userRepo, ILogUserLogin logUserLogin)
        {
            _userRepo = userRepo;
            _logUserLogin = logUserLogin;
        }
        public async Task<UserAddDTO> AddUser(UserAddDTO user)
        {
            User userToAdd = new User
            {
                firstName = user.firstName, 
                lastName = user.lastName,
                address = user.address,
                birthday = user.birthday,
                Email = user.email,
                PhoneNumber = user.phone,
                UserName = user.username,
                role = user.role,
                creationDate = DateTime.Now,
                updatedDate = DateTime.Now,
            };
            await _userRepo.Add(userToAdd);
            await _userRepo.SaveChanges();
            return user;
        }

        public async Task<bool> DeleteUser(string id)
        {
            User? user = await _userRepo.GetUserById(id);
            if (user == null)
            {
                return false;
            }
            await _userRepo.Delete(user);
            await _userRepo.SaveChanges();
            return true;
        }

        public async Task<IEnumerable<UserDTO>?> GetAllUsers()
        {
            IEnumerable<User> usersFromDB = await _userRepo.GetAllUsers();
            return usersFromDB.Select(user => new UserDTO
            {
                UserID =user.Id,
                firstName = user.firstName,
                lastName = user.lastName,
                address = user.address,
                birthday = user.birthday,
                email = user.Email,
                phone = user.PhoneNumber,
                username = user.UserName,
                role = user.role,
                creationDate = user.creationDate, updatedDate = user.updatedDate,
            });
        }

        public async Task<UserDTO?> GetUserById(string id)
        {
            User? user = await _userRepo.GetUserById(id);

            return new UserDTO
            {
                UserID = user.Id,
                firstName = user.firstName,
                lastName = user.lastName,
                address = user.address,
                birthday = user.birthday,
                email = user.Email,
                phone = user.PhoneNumber,
                username = user.UserName,
                role = user.role,
                creationDate = user.creationDate,
                updatedDate = user.updatedDate,
            };
        }

        public async Task<bool> UpdateUser(UserDTO user)
        {
            if (user.UserID is null)
            {
                return false;
            }
            User? DBuser = await _userRepo.GetUserById(user.UserID);
            if (DBuser == null)
            {
                return false;
            }
            DBuser.firstName = user.firstName;
            DBuser.lastName = user.lastName;
            DBuser.address = user.address;
            DBuser.UserName = user.username; 
            DBuser.role = user.role;
            DBuser.PhoneNumber = user.phone;
            DBuser.birthday = user.birthday;
            DBuser.Email = user.email;
            DBuser.updatedDate = DateTime.Now;
            await _userRepo.SaveChanges();
            return true;
        }





        public async Task LogUserLogin(string userId, string? username, string ipAddress, string userAgent, bool isSuccess)
        {
            await _logUserLogin.LogUserLogin(userId, username, ipAddress, userAgent, isSuccess);
        }

        public async Task<IEnumerable<UserLoginHistory>> GetLoginHistoryByUserId(string userId)
        {
            return await _logUserLogin.GetLoginHistoryByUserId(userId);
        }
    }
}
