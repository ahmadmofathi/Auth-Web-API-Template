using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using UAParser;

namespace TemplateWeb.DAL
{
    public class LogUserlogin : ILogUserLogin
    {
        private readonly AppDbContext _context;

        public LogUserlogin(AppDbContext context)
        {
            _context = context;
        }

        public async Task LogUserLogin(string userId, string? username, string ipAddress, string userAgent, bool isSuccess)
        {
            var parser = Parser.GetDefault();
            var clientInfo = parser.Parse(userAgent);

            string? os = clientInfo.OS.ToString();
            string? browser = clientInfo.UA.ToString();
            string? device = clientInfo.Device.IsSpider ? "Bot" :
                             clientInfo.Device.Family.ToLower().Contains("mobile") ? "Mobile" : "Desktop";

            string location = await GetLocationFromIP(ipAddress);

            var log = new UserLoginHistory
            {
                UserId = userId,
                Username = username,
                IPAddress = ipAddress,
                UserAgent = userAgent,
                LoginTime = DateTime.UtcNow,
                IsSuccess = isSuccess,
                Browser = browser,
                OperatingSystem = os,
                DeviceType = device,
                Location = location
            };

            await _context.LoginHistories.AddAsync(log);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UserLoginHistory>> GetLoginHistoryByUserId(string userId)
        {
            return await _context.LoginHistories
                .Where(l => l.UserId == userId)
                .OrderByDescending(l => l.LoginTime)
                .ToListAsync();
        }

        private async Task<string> GetLocationFromIP(string ip)
        {
            try
            {
                if (ip == "::1" || ip == "127.0.0.1") return "Localhost";

                using var client = new HttpClient();
                var response = await client.GetFromJsonAsync<IpApiResponse>($"http://ip-api.com/json/{ip}");
                if (response?.Status == "success")
                {
                    return $"{response.City}, {response.Country}";
                }
            }
            catch { }

            return "Unknown Location";
        }

    }
}
