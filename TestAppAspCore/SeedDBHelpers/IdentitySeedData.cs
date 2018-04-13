using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using QPD.PartialMenuLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Infrastructure;
using TestAppAspCore.Models;

namespace TestAppAspCore.SeedDBHelpers
{
    public static class IdentitySeedData
    {
        private struct UserInfo
        {
            public string UserName { get; set; }
            public string FullName { get; set; }
            public string Password { get; set; }
            public string RoleName { get; set; }
        }

        private static List<UserInfo> StandartUsers = new List<UserInfo>
        {
            new UserInfo
            {
                UserName = "admin@list.ru",
                FullName = "Администратор",
                Password = "admin",
                RoleName = RolesHelper.ADMIN_ROLE
            },
            new UserInfo
            {
                UserName = "bookkeeper@list.ru",
                FullName = "Библиотекарь",
                Password = "bookkeeper",
                RoleName = RolesHelper.BOOKKEEPER_ROLE
            },
            new UserInfo
            {
                UserName = "storekeeper@list.ru",
                FullName = "Кладовщик",
                Password = "storekeeper",
                RoleName = RolesHelper.STOREKEEPER_ROLE
            },
            new UserInfo
            {
                UserName = "vlasov_ms@list.ru",
                FullName = "Власов Максим Сергеевич",
                Password = "vlasov",
                RoleName = RolesHelper.USER_ROLE
            }
        };

        public static async Task<bool> EnsurePopulated(IServiceProvider service)
        {
            UserManager<User> userManager = service.GetRequiredService<UserManager<User>>();
            foreach(var info in StandartUsers)
            {            
                if (await userManager.FindByNameAsync(info.UserName) == null)
                {
                    User user = new User(info.UserName, info.FullName)
                    {
                        Email = info.UserName
                    };
                    user.SecurityStamp = Guid.NewGuid().ToString("D");
                    var result = await userManager.CreateAsync(user, info.Password);
                    await userManager.AddToRoleAsync(user, info.RoleName);
                }
            };
            return true;
        }

        public static async Task<bool> EnsureRoles(IServiceProvider service)
        {
            RoleManager<UserRole> roleManager = service.GetRequiredService<RoleManager<UserRole>>();

            var roles = typeof(RolesHelper).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                            .Select(x => (string)x.GetRawConstantValue())
                            .ToList();

            if (roleManager.Roles.Count() < roles.Count)
            {
                foreach(var roleName in roles)
                {
                    await roleManager.CreateAsync(new UserRole(roleName));
                }
            }
            return true;
        }

        public static async Task<bool> EnsureMenuElements(IServiceProvider service)
        {
            RoleManager<UserRole> roleManager = service.GetRequiredService<RoleManager<UserRole>>();
            IMenuElementRepository menuElementRepository = service.GetRequiredService<IMenuElementRepository>();

            var menuItems = typeof(RolesHelper).GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static)
                .Where(fi => fi.IsPrivate && fi.IsStatic && fi.FieldType == typeof(MenuItem))
                .Select(x => (MenuItem)x.GetValue(new MenuItem()))
                .ToList();

            //заполняем элементы меню в таблицу
            if (menuElementRepository.GetMenuElementsCount() < menuItems.Count)
                menuElementRepository.AddMenuElements(
                    menuItems.Select(elem => new MenuElement
                    {
                        Title = elem.Title,
                        AspAreaName = elem.AspAreaName,
                        AspActionName = elem.AspActionName,
                        AspControllerName = elem.AspControllerName
                    }).ToList());

            //производим соединение между ролью и элементами меню и заполняем таблицу элементов меню
            foreach (var menu in RolesHelper.RolesMenus)
            {
                var role = await roleManager.FindByNameAsync(menu.Key);
                if (menuElementRepository.GetMenuElementsForRole(role.Name).Count == 0)
                {
                    menuElementRepository.SetMenuElementsForRole(role, menu.Value.MenuItems
                                                                        .Select(elem =>
                                                                            new MenuElement(
                                                                            elem.Title,
                                                                            elem.AspAreaName,
                                                                            elem.AspControllerName,
                                                                            elem.AspActionName))
                                                                        .ToList());
                }
            }
            return true;
        }
    }
}
