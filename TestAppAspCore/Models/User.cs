using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }

        public User()
        {

        }

        public User(string userName, string fullName) : base(userName)
        {
            FullName = fullName;
        }
    }
}
