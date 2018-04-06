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
        private UserManager<User> _userManager;

        public TopMenuViewComponent(MenuService<MenuForRole> menuService, UserManager<User> userManager)
        {
            _menuService = menuService;
            _userManager = userManager;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.Name == null)
                throw new UnauthorizedAccessException("Пользователь не вошел в систему.");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count != 1)
                throw new Exception("Кол-во ролей пользователя должно быть равно 1."); 
            var menuItems = _menuService.GetMenuItems(RolesHelper.RolesMenus.GetValueOrDefault(roles[0]));
            return View(menuItems);
        }
    }
}
