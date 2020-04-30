using AutoMapper;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        //GET Api/Movies
        public IHttpActionResult GetMovies(string query=null)
        {
            var moviesQuery = _context.Movies.Include(m => m.genre).Where(m=>m.NumberAvailable>0);

            if(!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(c => c.Name.Contains(query));
            var movieDtos= moviesQuery.ToList().Select(Mapper.Map<Movie, MovieDto>);
            return Ok(movieDtos);
        }

        //GET Api/Movies/1
        public IHttpActionResult GetMovie(int id)
        {
            var dbMovie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (dbMovie == null)
            {
                return NotFound();
            }
            return Ok(Mapper.Map<Movie, MovieDto>(dbMovie));
        }

        //Post Api/Movies
        [HttpPost]
        public IHttpActionResult CreateMovie(MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var Movie = Mapper.Map<MovieDto, Movie>(MovieDto);
            _context.Movies.Add(Movie);
            _context.SaveChanges();
            MovieDto.Id = Movie.Id;
            return Created(new Uri(Request.RequestUri + "/" + MovieDto.Id), MovieDto);
        }

        //Put Api/Movies/1
        [HttpPut]
        public IHttpActionResult UpdateMovie(int id, MovieDto MovieDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var dbMovie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (dbMovie == null)
            {
                return NotFound();
            }

            Mapper.Map(MovieDto, dbMovie);
            _context.SaveChanges();
            return Ok();
        }

        //Delete Api/Movies/1
        [HttpDelete]
        public IHttpActionResult DeleteMovie(int id)
        {
            var dbMovie = _context.Movies.SingleOrDefault(x => x.Id == id);
            if (dbMovie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(dbMovie);
            _context.SaveChanges();
            return Ok();
        }
    }
}
