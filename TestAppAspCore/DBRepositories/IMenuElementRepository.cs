using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.DBRepositories
{
    public interface IMenuElementRepository : IDisposable
    {
        List<MenuElement> GetMenuElementsForRole(string roleName);
        void AddMenuElement(MenuElement elem);
        void AddMenuElements(List<MenuElement> elems);
        void SetMenuElementsForRole(UserRole role, List<MenuElement> elems);
        int GetMenuElementsCount();
    }
}
