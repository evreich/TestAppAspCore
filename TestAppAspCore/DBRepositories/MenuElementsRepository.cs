using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.EFCore;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public class MenuElementsRepository : IMenuElementRepository
    {
        private readonly BooksContext _context;

        public MenuElementsRepository(BooksContext context)
        {
            _context = context;
        }

        public int GetMenuElementIdByAttr(string title, string area, string controller, string action)
        {
            return _context.MenuElements
                .Where(elem =>
                elem.Title == title &&
                elem.AspAreaName == area &&
                elem.AspControllerName == controller &&
                elem.AspActionName == action)
                .SingleOrDefault()
                .MenuElementId;
        }

        public void AddMenuElement(MenuElement elem)
        {
            _context.MenuElements.Add(elem);
            _context.SaveChanges();
        }

        public void AddMenuElements(List<MenuElement> elems)
        {
            _context.MenuElements.AddRange(elems);
            _context.SaveChanges();
        }

        public int GetMenuElementsCount()
        {
            return _context.MenuElements.Count();
        }

        public List<MenuElement> GetMenuElementsForRole(string roleName)
        {
            return _context.UserRoleMenuElements
                .Include(item => item.UserRole)
                .Join(_context.MenuElements,
                      um => um.MenuElementId,
                      me => me.MenuElementId,
                      (um, me) => new
                      {
                          roleName = um.UserRole.Name,
                          menuElemId = me.MenuElementId,
                          aspArea = me.AspAreaName,
                          aspController = me.AspControllerName,
                          aspAction = me.AspActionName,
                          menuTitle = me.Title
                      })
                .Where(item => item.roleName == roleName)
                .Select(item => new MenuElement(item.menuTitle, item.aspArea, item.aspController, item.aspAction)).ToList();   
        }

        public void SetMenuElementsForRole(UserRole role, List<MenuElement> elems)
        {
            elems.ForEach((MenuElement elem) =>
            {
                var menuElementId = GetMenuElementIdByAttr(elem.Title, elem.AspAreaName, elem.AspControllerName, elem.AspActionName);

                _context.UserRoleMenuElements.Add(
                new UserRoleMenuElement
                {
                    MenuElementId = menuElementId,
                    UserRoleId = role.Id
                });
            });
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
