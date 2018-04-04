using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TestAppAspCore.ViewModels
{
    public class ChangeRoleViewModel
    {
        public string UserId { get; set; }
        public string UserEmail { get; set; }
        public IEnumerable<SelectListItem> AllRoles { get; set; }
        public ChangeRoleViewModel()
        {
            AllRoles = new List<SelectListItem>();
        }
    }
}
