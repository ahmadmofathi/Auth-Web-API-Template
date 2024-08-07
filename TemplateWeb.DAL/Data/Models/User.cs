using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWeb.DAL
{
    public class User : IdentityUser<string>
    {
        public string firstName {  get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string role { get; set; }
        public string birthday { get; set; }
        public string creationDate { get; set; }
        public string updatedDate { get; set; }
    }
}
