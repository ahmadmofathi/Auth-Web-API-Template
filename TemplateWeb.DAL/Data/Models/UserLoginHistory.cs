using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWeb.DAL
{
    public class UserLoginHistory
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string? Username { get; set; }
        public DateTime LoginTime { get; set; }
        public string? IPAddress { get; set; }
        public string? UserAgent { get; set; }
        public string? DeviceType { get; set; }
        public string? Browser { get; set; }
        public string? OperatingSystem { get; set; }
        public string? Location { get; set; }
        public bool IsSuccess { get; set; }
    }

}
