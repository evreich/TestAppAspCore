using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.ViewModels
{
    public class ShowUserViewModel
    {
        public string RoleName { get; set; }
        public User User { get; set; }
    }
}
