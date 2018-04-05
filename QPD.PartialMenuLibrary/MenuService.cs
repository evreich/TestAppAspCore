using System;
using System.Collections.Generic;
using System.Text;

namespace QPD.PartialMenuLibrary
{
    public class MenuService<T> where T : MenuForRole
    {
        public MenuService()
        {
        }

        public List<MenuItem> GetMenuItems(T menu) => menu.MenuItems;
    }
}
