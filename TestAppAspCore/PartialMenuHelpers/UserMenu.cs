using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.PartialMenuHelpers
{
    public class UserMenu : MenuForRole
    {
        public UserMenu() : base(RolesHelper.USER_ROLE)
        {
            List<MenuItem> menuItems = new List<MenuItem>
            {
                new MenuItem("Books Market",
                    nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                    nameof(Areas.Market.Controllers.HomeController.ShowBooks),
                    nameof(Areas.Market))
            };

            SetMenuItems(menuItems);
        }
    }
}
