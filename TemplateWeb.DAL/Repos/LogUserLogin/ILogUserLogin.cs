using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWeb.DAL
{
    public interface ILogUserLogin
    {
        Task LogUserLogin(string userId, string? username, string ipAddress, string userAgent, bool isSuccess);
        Task<IEnumerable<UserLoginHistory>> GetLoginHistoryByUserId(string userId);
    }
}
