using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.PartialMenuHelpers
{
    public class AdminMenu : MenuForRole
    {
        public AdminMenu() : base(RolesHelper.ADMIN_ROLE)
        {
            List<MenuItem> menuItems = new List<MenuItem>
            {
                new MenuItem("Books Market",
                    nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                    nameof(Controllers.HomeController.Index)),
                new MenuItem("Книги",
                    nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                    nameof(Controllers.HomeController.Index)),
                new MenuItem("Жанры",
                    nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                    nameof(Controllers.HomeController.ShowGenres)),
                new MenuItem("Пользователи",
                    nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                    nameof(Controllers.HomeController.ShowUsers))
            };

            SetMenuItems(menuItems);
        }
    }
}
