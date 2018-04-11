using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.PartialMenuHelpers
{
    public class UserMenu : MenuForRole
    {
        private static List<MenuItem> menuItems = new List<MenuItem>
        {
            new MenuItem("Books Market",
                nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Areas.Market.Controllers.HomeController.ShowBooks),
                nameof(Areas.Market)),
            new MenuItem("Книги",
                nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Areas.Market.Controllers.HomeController.ShowBooks),
                nameof(Areas.Market)),
            new MenuItem("Заказы",
                nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Areas.Market.Controllers.HomeController.ShowOrders),
                nameof(Areas.Market)),
            new MenuItem("Хранилище",
                nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Areas.Market.Controllers.HomeController.ShowStore),
                nameof(Areas.Market))
        };

        public UserMenu() : base(RolesHelper.USER_ROLE, menuItems)
        {
        }
    }
}
