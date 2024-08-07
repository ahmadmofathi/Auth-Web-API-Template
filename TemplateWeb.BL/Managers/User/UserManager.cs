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


        public UserManager(IUserRepo userRepo)
        {
            _userRepo = userRepo;

        }
        public string AddUser(UserAddDTO user)
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
                creationDate = DateTime.Now.ToString(),
                updatedDate = DateTime.Now.ToString(),
            };
            _userRepo.Add(userToAdd);
            _userRepo.SaveChanges();
            return "User: " + userToAdd.UserName + " is added successfully Id: " + userToAdd.Id;
        }

        public bool DeleteUser(string id)
        {
            User? user = _userRepo.GetUserById(id);
            if (user == null)
            {
                return false;
            }
            _userRepo.Delete(user);
            _userRepo.SaveChanges();
            return true;
        }

        public IEnumerable<UserDTO> GetAllUsers()
        {
            IEnumerable<User> usersFromDB = _userRepo.GetAllUsers();
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

        public UserDTO? GetUserById(string id)
        {
            User? user = _userRepo.GetUserById(id);

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

        public bool UpdateUser(UserDTO user)
        {
            if (user.UserID is null)
            {
                return false;
            }
            User? DBuser = _userRepo.GetUserById(user.UserID);
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
            DBuser.updatedDate = DateTime.Now.ToString();
            _userRepo.SaveChanges();
            return true;
        }
    }
}
