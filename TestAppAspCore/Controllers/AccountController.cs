using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User(model.Email, model.FullName)
                {
                    Email = model.Email,
                    PhoneNumber = model.Phone
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, RolesHelper.USER_ROLE);
                    // установка куки
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction(nameof(Areas.Market.Controllers.HomeController.ShowBooks), 
                        nameof(Areas.Market.Controllers.HomeController).Replace("Controller", ""), new { area = nameof(Areas.Market)});
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    var role = await _userManager.GetRolesAsync(await _userManager.FindByEmailAsync(model.Email));
                    if (role.Count != 1)
                    {
                        throw new Exception("Ошибка! Пользователь должен обладать одной ролью.");
                    };

                    var startpage = RolesHelper.RoleStartPages.GetValueOrDefault(role[0]);
                    return RedirectPermanent(startpage);
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> LogOff()
        {
            // удаляем аутентификационные куки
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AccountController.Login), nameof(AccountController).Replace("Controller", ""));
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = RolesHelper.ADMIN_ROLE)]
        public async Task<IActionResult> ChangeRole(string userId)
        {
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                if (userRoles.Count != 1)
                    return RedirectToAction(nameof(HomeController.ServerError), nameof(HomeController).Replace("Controller",string.Empty));
                var allRoles = _roleManager.Roles.ToList();
                ChangeRoleViewModel model = new ChangeRoleViewModel
                {
                    UserId = user.Id,
                    UserEmail = user.Email,
                    AllRoles = new SelectList(allRoles, nameof(IdentityRole.Name), nameof(IdentityRole.Name),allRoles.FirstOrDefault(role => role.Name == userRoles[0])) 
                };
                return View(model);
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RolesHelper.ADMIN_ROLE)]
        public async Task<IActionResult> ChangeRole(string userId, string currRole)
        {
            // получаем пользователя
            User user = await _userManager.FindByIdAsync(userId);
            if (user != null)
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var oldRole = _roleManager.Roles.FirstOrDefault(role => role.Name == userRoles[0]).Name;

                await _userManager.AddToRolesAsync(user, new List<string> { currRole });
                await _userManager.RemoveFromRolesAsync(user, new List<string> { oldRole });

                return RedirectToAction(nameof(HomeController.ShowUsers),nameof(HomeController).Replace("Controller", string.Empty));
            }

            return NotFound();
        }
    }
}