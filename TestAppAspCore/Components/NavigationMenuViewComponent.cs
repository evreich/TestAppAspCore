using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TestAppAspCore.Components
{
    public class NavigationMenuViewComponent : ViewComponent
    {
        private IGenresRepository repository;
        public NavigationMenuViewComponent(IGenresRepository rep)
        {
            repository = rep;
        }
        public IViewComponentResult Invoke()
        {
            //ViewBag.SelectedCategory = RouteData?.Values["category"];
            return View(repository.GetAllGenres().OrderBy(genre => genre.Title));
        }
    }
}
