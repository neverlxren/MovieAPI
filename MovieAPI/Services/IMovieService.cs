using MovieAPI.DTOs;
using MovieAPI.Models;

namespace MovieAPI.Services;

public interface IMovieService
{
    Task<MovieResponseDto?> GetMovieById(int id);
    
    
    Task<IEnumerable<MovieResponseDto>> GetMovieByTitle(string title);

    Task<IEnumerable<MovieResponseDto>>
        GetMoviesFiltered(string? genre, string? director, decimal? rating, string? country, short? releaseYear);


    Task<IEnumerable<MovieResponseDto>> GetAllMovies();

}