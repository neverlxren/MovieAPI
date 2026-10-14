using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using MovieAPI.Data;
using MovieAPI.DTOs;
using MovieAPI.Models;

namespace MovieAPI.Services;

public class MovieService: IMovieService
{
    private readonly MovieDbContext _movieDbContext;

    public MovieService(MovieDbContext dbContext)
    { 
        _movieDbContext = dbContext;
    }
    
    public async Task<IEnumerable<MovieResponseDto>> GetMoviesFiltered(
        string? genre,
        string? director,
        decimal? rating,
        string? country,
        short? releaseYear)
    {
        var query = _movieDbContext.Movies.AsQueryable();

        if (!string.IsNullOrWhiteSpace(genre))
            query = query.Where(m => m.Genre == genre);

        if (!string.IsNullOrWhiteSpace(director))
            query = query.Where(m => m.Director == director);

        if (!string.IsNullOrWhiteSpace(country))
            query = query.Where(m => m.Country == country);

        if (rating.HasValue)
            query = query.Where(m => m.Rating == rating.Value);

        if (releaseYear.HasValue)
            query = query.Where(m => m.ReleaseYear == releaseYear.Value);

        var movies = await query
            .Select(m => ToDto(m))
            .ToListAsync();

        return movies;
    }


    public async Task<MovieResponseDto?> GetMovieById(int id)
    {
        var movie = await _movieDbContext.Movies.FindAsync(id);

        if (movie is null) return null;

        return ToDto(movie);
    }

    public async Task<IEnumerable<MovieResponseDto>> GetMovieByTitle(string title)
    {
        var movies = await _movieDbContext.Movies
            .Where(x => title == x.Title)
            .Select(x => ToDto(x))
            .ToListAsync();
        
        // Any, Find (pk), Single(), Where().ToList();, Single(), First()
        

        return movies;
    }

    public async Task<IEnumerable<MovieResponseDto>> GetAllMovies()
    {
        var movies = await _movieDbContext.Movies.Select(x => ToDto(x)).ToListAsync();

        return movies;
    }

    //---------Converter-----------
    private static MovieResponseDto ToDto(Movie movie) => new()
    {
        Country = movie.Country,
        Genre = movie.Genre,
        ReleaseYear = movie.ReleaseYear,
        Director = movie.Director,
        DurationMinutes = movie.DurationMinutes,
        Language = movie.Language,
        Id = movie.Id,
        Rating = movie.Rating,
        Title = movie.Title
    };
}


