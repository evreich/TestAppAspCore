using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.PartialMenuHelpers
{
    public class BookKeeperMenu: MenuForRole
    {
        public BookKeeperMenu() : base(RolesHelper.BOOKKEEPER_ROLE)
        {
            List<MenuItem> menuItems = new List<MenuItem>
            {
                new MenuItem("Books Market",
                    nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                    nameof(Controllers.HomeController.ShowOrders)),
                new MenuItem("Заказы",
                    nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                    nameof(Controllers.HomeController.ShowOrders)),
            };

            SetMenuItems(menuItems);
        }
    }
}
