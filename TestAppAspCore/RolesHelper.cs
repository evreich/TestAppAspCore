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
        private const string USER_START_PAGE = "/Market/Home/ShowBooks";

        public const string ADMIN_ROLE = "admin";
        private const string ADMIN_START_PAGE = "/Home/Index";

        public const string BOOKKEEPER_ROLE = "bookKeeper";
        private const string BOOKKEEPER_START_PAGE = "/Home/ShowOrders";

        public const string STOREKEEPER_ROLE = "storeKeeper";
        private const string STOREKEEPER_START_PAGE = "/Home/Index";
        
        public static readonly Dictionary<string, MenuForRole> RolesMenus = new Dictionary<string, MenuForRole>
        {
            { USER_ROLE, new UserMenu() },
            { ADMIN_ROLE, new AdminMenu() },
            { BOOKKEEPER_ROLE, new BookKeeperMenu() },
            { STOREKEEPER_ROLE, new StoreKeeperMenu() }
        };

        public static readonly Dictionary<string, string> RoleStartPages = new Dictionary<string, string>
        {
            { USER_ROLE, USER_START_PAGE },
            { ADMIN_ROLE, ADMIN_START_PAGE },
            { BOOKKEEPER_ROLE, BOOKKEEPER_START_PAGE },
            { STOREKEEPER_ROLE, STOREKEEPER_START_PAGE }
        };

    }
}
