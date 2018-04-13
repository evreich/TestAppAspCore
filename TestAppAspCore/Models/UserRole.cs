using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class UserRole : IdentityRole
    {
        public IList<UserRoleMenuElement> UserRoleMenuElements { get; set; } = new List<UserRoleMenuElement>();

        public UserRole(string roleName) : base(roleName) { }

        public UserRole():base() { }
    }
}
