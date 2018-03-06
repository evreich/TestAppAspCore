using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TestAppAspCore.DBRepositories;
using TestAppAspCore.Models;
using TestAppAspCore.ViewModels;

namespace TestAppAspCore.Controllers
{
    public class GenreController : Controller
    {
        IGenresRepository _genresRepository;

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
            try
            {
                if (ModelState.IsValid)
                {
                    _genresRepository.AddGenre(new Genre { Title = genre.Title });
                    return RedirectToAction(nameof(HomeController.ShowGenres), nameof(HomeController).Replace("Controller", ""));
                }
                else
                {
                    return View(genre);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Genre/Edit/{id}
        [HttpGet]
        public ActionResult Edit(int id)
        {
            try
            {
                var genre = _genresRepository.GetGenre(id);

                return View(genre);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: Genre/Edit/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Genre genre)
        {
            try
            {
                if (genre.Id <= 0)
                    throw new ArgumentException("Genre ID is empty or <= 0");
                if (ModelState.IsValid)
                {
                    _genresRepository.EditGenre(genre);
                    return RedirectToAction(nameof(HomeController.ShowGenres), nameof(HomeController).Replace("Controller", ""));
                }
                else
                {
                    return View(genre);
                }

            }
            catch (Exception)
            {
                return View();
            }
        }

        // GET: Genre/Delete/{id}
        [HttpGet]
        public ActionResult Delete(int id)
        {
            try
            {
                var genre = _genresRepository.GetGenre(id);

                return View(genre);
            }
            catch (Exception)
            {
                return View();
            }
        }

        // POST: Genre/Delete/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(Genre genre)
        {
            try
            {
                if (genre.Id <= 0)
                    throw new ArgumentException("Genre ID is empty or <= 0");
                if (ModelState.IsValid)
                {
                    _genresRepository.DeleteGenre(genre);
                    return RedirectToAction(nameof(HomeController.ShowGenres), nameof(HomeController).Replace("Controller", ""));
                }
                else
                {
                    return View(genre);
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}