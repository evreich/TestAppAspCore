using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class User : IdentityUser
    {
        [Required]
        public string FullName { get; set; }

        public ICollection<Order> Orders { get; set; } = new List<Order>();

        public User()
        {

        }

        public User(string userName, string fullName) : base(userName)
        {
            FullName = fullName;
        }
    }
}
