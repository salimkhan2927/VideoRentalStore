using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Vidly.Models;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    [Authorize]
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        public ActionResult Random()
        {
            return View();
        }

        //--------------------------GET: Movies-----------------------------------------
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");
            return View("ReadOnlyList");
        }

        //---------------------------------Create Or Update------------------
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Save(Movie movie)
        {
            if(!ModelState.IsValid)
            {
                MovieFormViewModel movieVM = new MovieFormViewModel(movie)
                {
                    genres=_context.Genres.ToList()
                };
                return View("MovieFormViewModel",movieVM);
            }
            if (movie.Id == 0)
            {
                _context.Movies.Add(movie);
            }
            else
            {
                var dbMovie = _context.Movies.Single(m => m.Id == movie.Id);

                dbMovie.Name = movie.Name;
                dbMovie.GenreId = movie.GenreId;
                dbMovie.ReleaseDate = movie.ReleaseDate;
                dbMovie.NumberInStock = movie.NumberInStock;
                dbMovie.DateAdded = movie.DateAdded;
            }
                _context.SaveChanges();
            return RedirectToAction("Index", "Movies");
        }

        //----------------------------------Update Movie------
        public ActionResult Edit(int id)
        {
            ViewBag.New = "Edit";
            var movie = _context.Movies.SingleOrDefault(c => c.Id == id);
            if (movie == null)
                return HttpNotFound();
            var viewModel = new MovieFormViewModel(movie)
            {
                genres = _context.Genres.ToList()
            };
            return View("MovieFormViewModel", viewModel);
        }

        //-------------------------------Provides Form to Create New Movie or Update Existing Movie-----------------
        [Authorize(Roles=RoleName.CanManageMovies)]
        public ActionResult MovieFormViewModel()
        {
            var MovieVM = new MovieFormViewModel()
            {
                genres = _context.Genres.ToList()
            };
            return View(MovieVM);
        }
    }
    
}