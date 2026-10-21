using System.Diagnostics;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using MovieApi.Web.Models;
using MovieApi.Web.Services;

namespace MovieApi.Web.Controllers;

public class HomeController : Controller
{
    private readonly IMovieServices _movieServices;

    private readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true
    };
    
    public HomeController(IMovieServices movieServices)
    {
        _movieServices = movieServices; 
    }


    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    public async Task<IActionResult> MovieList()
    {
        var result = await _movieServices.GetAllMovies(); // server zdoh, inet off, api stucked, endpoint wrong

        if (!result.IsSuccessful)
        {
            ViewBag.Error = $"error with status code: {result.StatusCode}";
            return View(new List<MovieViewModel>());
        }

        var movies = string.IsNullOrWhiteSpace(result.RawMessage)
            ? new List<MovieViewModel>()
            : JsonSerializer.Deserialize<List<MovieViewModel>>(result.RawMessage, JsonOptions) ?? new List<MovieViewModel>();

        return View(movies);
        
        // variable = method ?? "default";
        // ternar operator
        // (statement) ? actions_if_true : action_if_false; return (isready) ? true :  false

    }

    // public async Task<IActionResult> MovieDetails(int id)
    // {
    //     var result = await _movieServices.GetById(id);
    //
    //     if (!result.IsSuccessful)
    //     {
    //         return NotFound();
    //     }
    //
    //     var movie = JsonSerializer.Deserialize<MovieViewModel>(result.RawMessage, JsonOptions);
    //     return View(movie);
    // }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

