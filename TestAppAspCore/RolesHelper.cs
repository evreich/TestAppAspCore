using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using QPD.PartialMenuLibrary;

namespace TestAppAspCore
{
    public static class RolesHelper
    {
        private static readonly MenuItem Admin_BookStoreItem = 
                new MenuItem("Books Market",
                nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Controllers.HomeController.Index));

        private static readonly MenuItem Storekeeper_BooksItem =
                new MenuItem("Книги",
                nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Controllers.HomeController.Index));

        private static readonly MenuItem Storekeeper_GenresItem =
                new MenuItem("Жанры",
                nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Controllers.HomeController.ShowGenres));

        private static readonly MenuItem Bookkeeper_OrdersItem =
                new MenuItem("Заказы",
                nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Controllers.HomeController.ShowOrders));

        private static readonly MenuItem Bookkeeper_ReturnBookItem =
                new MenuItem("Возврат",
                nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Controllers.HomeController.ShowReturnedBooks));

        private static readonly MenuItem User_BookMarketItem =
                new MenuItem("Books Market",
                nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Areas.Market.Controllers.HomeController.ShowBooks),
                nameof(Areas.Market));

        private static readonly MenuItem User_BookItem =
                new MenuItem("Книги",
                nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Areas.Market.Controllers.HomeController.ShowBooks),
                nameof(Areas.Market));

        private static readonly MenuItem User_OrdersItem =
                new MenuItem("Заказы",
                nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Areas.Market.Controllers.HomeController.ShowOrders),
                nameof(Areas.Market));

        private static readonly MenuItem User_StoreItem =
                new MenuItem("Хранилище",
                nameof(Areas.Market.Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Areas.Market.Controllers.HomeController.ShowStore),
                nameof(Areas.Market));

        private static readonly MenuItem Bookkeeper_StoreItem = new MenuItem("Books Market",
                nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Controllers.HomeController.ShowOrders));

        private static readonly MenuItem Admin_StoreItem = new MenuItem("Пользователи",
                nameof(Controllers.HomeController).Replace("Controller", string.Empty),
                nameof(Controllers.HomeController.ShowUsers));

        private static List<MenuItem> userMenuItems = new List<MenuItem>
        {
            User_BookMarketItem,
            User_BookItem,
            User_OrdersItem,
            User_StoreItem
        };

        private static List<MenuItem> bookkeeperMenuItems = new List<MenuItem>
        {
            Bookkeeper_StoreItem,
            Bookkeeper_OrdersItem,
            Bookkeeper_ReturnBookItem
        };

        private static List<MenuItem> storekeeperMenuItems = new List<MenuItem>
        {
            Admin_BookStoreItem,
            Storekeeper_BooksItem,
            Storekeeper_GenresItem
        };

        private static List<MenuItem> adminMenuItems = new List<MenuItem>
        {
            Admin_BookStoreItem,
            Storekeeper_BooksItem,
            Storekeeper_GenresItem,
            Bookkeeper_OrdersItem,
            Bookkeeper_ReturnBookItem,
            Admin_StoreItem
        };

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
            { USER_ROLE, new MenuForRole(USER_ROLE, userMenuItems)},
            { ADMIN_ROLE, new MenuForRole(ADMIN_ROLE, adminMenuItems) },
            { BOOKKEEPER_ROLE, new MenuForRole(BOOKKEEPER_ROLE, bookkeeperMenuItems) },
            { STOREKEEPER_ROLE, new MenuForRole(STOREKEEPER_ROLE, storekeeperMenuItems) }
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
