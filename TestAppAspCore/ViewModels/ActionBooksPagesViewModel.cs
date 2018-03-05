using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAppAspCore.Models;

namespace TestAppAspCore.ViewModels
{
    public class ActionBooksPagesViewModel
    {
        public IEnumerable<SelectListItem> Genres { get; set; }
        public BookViewModel Book { get; set; }
    }
}
