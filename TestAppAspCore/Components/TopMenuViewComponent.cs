using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;

namespace TestAppAspCore.Components
{
    public class TopMenuViewComponent : ViewComponent
    {
        //private MenuService<MenuForRole> _menuService;
        private UserManager<User> _userManager;
        private IMenuElementRepository _menuElementsRepository;

        public TopMenuViewComponent(UserManager<User> userManager, IMenuElementRepository menuElementRepository /*MenuService<MenuForRole> menuService*/)
        {
            //_menuService = menuService;
            _userManager = userManager;
            _menuElementsRepository = menuElementRepository;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            if (User.Identity.Name == null)
                throw new UnauthorizedAccessException("Пользователь не вошел в систему.");
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Count != 1)
                throw new Exception("Кол-во ролей пользователя должно быть равно 1.");

            List<MenuElement> menuItems = _menuElementsRepository.GetMenuElementsForRole(roles[0]);
            /*var menuItems = _menuService.GetMenuItems(RolesHelper.RolesMenus.GetValueOrDefault(roles[0])); */
            return View(menuItems);
        }
    }
}
