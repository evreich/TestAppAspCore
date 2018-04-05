using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.SeedDBHelpers
{
    public static class IdentitySeedData
    {
        private const string _adminUser = "vlasov_ms@list.ru";
        private const string _adminFullName = "Власов Максим";
        private const string _adminPassword = "PaSsWoRd123";
        public static async void EnsurePopulated(IApplicationBuilder app)
        {
            UserManager<User> userManager = app.ApplicationServices.GetRequiredService<UserManager<User>>();
            User user = await userManager.FindByIdAsync(_adminUser);
            if (user == null)
            {
                user = new User(_adminUser, _adminFullName)
                {
                    Email = _adminUser
                };
                await userManager.CreateAsync(user, _adminPassword);
                user.SecurityStamp = Guid.NewGuid().ToString("D");
                await userManager.AddToRoleAsync(user, RolesHelper.ADMIN_ROLE);
            }
        }

        public static async void EnsureRoles(IApplicationBuilder app)
        {
            RoleManager<IdentityRole> roleManager = app.ApplicationServices.GetRequiredService<RoleManager<IdentityRole>>();
            if (roleManager.Roles.Count() < 1)
            {
                foreach(var constField in typeof(RolesHelper).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                            .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(string))
                            .Select(x => (string)x.GetRawConstantValue())
                            .ToList())
                {
                    await roleManager.CreateAsync(new IdentityRole(constField));
                }
            }
        }
    }
}
