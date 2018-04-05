using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Controllers
{
    [Authorize(Roles = RolesHelper.ADMIN_ROLE)]
    public class GenreController : Controller
    {
        private readonly IGenresRepository _genresRepository;

        public GenreController(IGenresRepository genresRepository)
        {
            _genresRepository = genresRepository;
        }

        // GET: Genre/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genre/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Genre genre)
        {
            if (ModelState.IsValid)
            {
                _genresRepository.AddGenre(new Genre { Title = genre.Title });
                TempData["message"] = $"Жанр \"{genre.Title}\" успешно добавлен";
                return RedirectToAction(nameof(HomeController.ShowGenres), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                return View(genre);
            }
        }

        // GET: Genre/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var genre = _genresRepository.GetGenre(id);

            return View(genre);
        }

        // POST: Genre/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)
        {
            if (genre.Id <= 0)
                throw new ArgumentException("Genre ID is empty or <= 0");
            if (ModelState.IsValid)
            {
                _genresRepository.EditGenre(genre);
                TempData["message"] = $"Жанр \"{genre.Title}\" успешно изменен";
                return RedirectToAction(nameof(HomeController.ShowGenres), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                return View(genre);
            }
        }

        // GET: Genre/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var genre = _genresRepository.GetGenre(id);

            return View(genre);
        }

        // POST: Genre/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Genre genre)
        {
            if (genre.Id <= 0)
                throw new ArgumentException("Genre ID is empty or <= 0");
            if (ModelState.IsValid)
            {
                _genresRepository.DeleteGenre(genre);
                TempData["message"] = $"Жанр \"{genre.Title}\" успешно удален";
                return RedirectToAction(nameof(HomeController.ShowGenres), nameof(HomeController).Replace("Controller", ""));
            }
            else
            {
                return View(genre);
            }
        }
    }
}