namespace MovieApi.Web.Services;

public interface IMovieServices
{
     Task<ApiResult> GetById(int id);
     Task<ApiResult> GetAllMovies(); 


}

// API -> WebView : IService, ApiResult -> IsSuccess, StatusCode, Message?; Service; create VM; Views; Controller(how views works)

public class MovieService : IMovieServices
{
     private readonly HttpClient _httpClient;

     public MovieService(HttpClient httpClient)
     {
          _httpClient = httpClient;
     }
     
     
     public async Task<ApiResult> GetById(int id)
     {
          // response, payload, content?

          var response = await _httpClient.GetAsync($"api/view/movie/{id}");

          var data = await response.Content.ReadAsStringAsync();

          return new ApiResult()
          {
               IsSuccessful = response.IsSuccessStatusCode,
               StatusCode = (int)response.StatusCode,
               RawMessage = data
          };
          
          //todo: views templates 
     }

     public async Task<ApiResult> GetAllMovies()
     {
          
          var response = await _httpClient.GetAsync($"api/view/movies");

          var data = await response.Content.ReadAsStringAsync();

          return new ApiResult()
          {
               IsSuccessful = response.IsSuccessStatusCode,
               StatusCode = (int)response.StatusCode,
               RawMessage = data
          };
     }
}



public class ApiResult
{
     public bool IsSuccessful { get; set; }
     public int StatusCode { get; set; }
     public string? RawMessage { get; set; }
}