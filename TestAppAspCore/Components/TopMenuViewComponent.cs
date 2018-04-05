using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.Components
{
    public class TopMenuViewComponent : ViewComponent
    {
        private MenuService<MenuForRole> _menuService;

        public TopMenuViewComponent(MenuService<MenuForRole> menuService)
        {
            _menuService = menuService;
        }

        public async Task<IViewComponentResult> Invoke([FromServices] UserManager<User> userManager)
        {
            var user = await userManager.FindByNameAsync(User.Identity.Name);
            var roles = await userManager.GetRolesAsync(user);
            if (roles.Count != 1)
                throw new Exception("Кол-во ролей пользователя должно быть равно 1."); 
            var menuItems = _menuService.GetMenuItems(RolesHelper.RolesMenus.GetValueOrDefault(roles[0]));
            return View(menuItems);
        }
    }
}
