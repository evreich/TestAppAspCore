using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class UserRoleMenuElement
    {
        public int MenuElementId { get; set; }
        public MenuElement MenuElement { get; set; }
        public string UserRoleId { get; set; }
        public UserRole UserRole { get; set; }
    }
}
