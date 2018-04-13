using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.Models
{
    public class MenuElement : MenuItem
    {
        public int MenuElementId { get; set; }
        public IList<UserRoleMenuElement> UserRoleMenuElements { get; set; } = new List<UserRoleMenuElement>();

        public MenuElement() : base() { }

        public MenuElement(string title, string contollerName, string actionName) : base(title, contollerName, actionName) { }

        public MenuElement(string title, string areaName, string contollerName, string actionName) : 
            base(title, contollerName, actionName, areaName) { }
    }
}
