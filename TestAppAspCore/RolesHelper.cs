using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QPD.PartialMenuLibrary;
using TestAppAspCore.PartialMenuHelpers;

namespace TestAppAspCore
{
    public static class RolesHelper
    {
        public const string USER_ROLE = "user";
        public const string ADMIN_ROLE = "admin";
        public const string BOOKKEEPER_ROLE = "bookKeeper";
        public const string STOREKEEPER_ROLE = "storeKeeper";
         
        public static readonly Dictionary<string, MenuForRole> RolesMenus = new Dictionary<string, MenuForRole>
        {
            { USER_ROLE, new UserMenu() },
            { ADMIN_ROLE, new AdminMenu() },
            { BOOKKEEPER_ROLE, new BookKeeperMenu() },
            { STOREKEEPER_ROLE, new StoreKeeperMenu() }
        };

    }
}
