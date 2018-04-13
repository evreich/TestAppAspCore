using System;
using System.Collections.Generic;
using System.Text;

namespace QPD.PartialMenuLibrary
{
    public class MenuForRole
    {
        public string RoleName { get; }
        public List<MenuItem> MenuItems { get; } = new List<MenuItem>();

        public MenuForRole(string roleName, List<MenuItem> menu)
        {
            RoleName = roleName;
            SetMenuItems(menu);
        }

        protected void SetMenuItems(List<MenuItem> items)
        {
            if (items.Count > 0)
            {
                MenuItems.AddRange(items);
            }
            else
                throw new ArgumentException("Items don`t have elements");
        }
    }
}
