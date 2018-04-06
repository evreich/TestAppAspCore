using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TestAppAspCore.Controllers
{
    [Authorize(Roles = RolesHelper.ADMIN_ROLE + "," + RolesHelper.BOOKKEEPER_ROLE)]
    public class OrderController : Controller
    {
        [HttpPost]
        public IActionResult ConfirmOrder(int orderID)
        {
            return Json(new { });
        }

        [HttpGet]
        public IActionResult GetOrder(int orderID)
        {
            return Json(new { });
        }

        [HttpGet]
        public IActionResult EditOrder(int orderID)
        {
            return Json(new { });
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult EditOrder(/*OrderViewModel order*/)
        {
            return Json(new { });
        }

        [HttpPost]
        public IActionResult DeleteOrder(int orderID)
        {
            return Json(new { });
        }
    }
}