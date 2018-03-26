using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;


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
            ViewBag.SelectedGenre = RouteData?.Values["genre"];
            return View(repository.GetAllGenres().OrderBy(genre => genre.Title));
        }
    }
}
