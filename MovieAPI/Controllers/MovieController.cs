using Microsoft.AspNetCore.Mvc;
using MovieAPI.DTOs;
using MovieAPI.Services;

namespace MovieAPI.Controllers;
[ApiController]
[Route("api/")]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;

    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    [HttpGet("view/movie/{id:int}")]
    public async Task<IActionResult> GetMovieById(int id)
    {
        var result = await _movieService.GetMovieById(id);
        if (result is null) return NotFound();

        return Ok(result);
    }
    [HttpGet("view/movie/{title}")]
    public async Task<IActionResult> GetMovieByTitle(string title)
    {
        var result = await _movieService.GetMovieByTitle(title);

        return Ok(result);
        
    }
    [HttpGet("view/movie/genre={genre}&director={director}&rating={rating}&country={country}&releaseYear={releaseYear}")]
    public async Task<IActionResult> GetMoviesFiltred(string? genre, string? director, decimal? rating, string? country,
        short? releaseYear)
    {
        var result = await _movieService.GetMoviesFiltered(genre, director, rating, country, releaseYear);

        return Ok(result);
    }
    [HttpGet("view/movies")]
    public async Task<IActionResult> GetAllMovies()
    {
        var result = await _movieService.GetAllMovies();

        return Ok(result);
    }
}