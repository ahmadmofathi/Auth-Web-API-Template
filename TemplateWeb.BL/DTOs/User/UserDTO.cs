﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateWeb.BL
{
    public class UserDTO
    {
        public string UserID { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string username { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public string role { get; set; }
        public string birthday { get; set; }
        public string creationDate { get; set; }
        public string updatedDate { get; set; }
    }
}
