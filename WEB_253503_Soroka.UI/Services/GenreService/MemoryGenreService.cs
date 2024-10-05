using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;

namespace WEB_253503_Soroka.UI.Services.GenreService;

public class MemoryGenreService : IGenreService
{
    public Task<ResponseData<List<Genre>>> GetGenreListAsync()
    {
        var genresList = new List<Genre>
        {
            new Genre
            {
                Id = 1,
                Name = "Ужасы",
                NormalizedName = "horror",
            },
            new Genre
            {
                Id = 2,
                Name = "Триллер",
                NormalizedName = "thriller",
            },
            new Genre
            {
                Id = 3,
                Name = "Драма",
                NormalizedName = "drama",
            },
            new Genre
            {
                Id = 4,
                Name = "Фантастика",
                NormalizedName = "fantasy",
            },
            new Genre
            {
                Id = 5,
                Name = "Комедия",
                NormalizedName = "comedy",
            },
            new Genre
            {
                Id = 6,
                Name = "Приключения",
                NormalizedName = "adventure",
            }
        };

        var result = ResponseData<List<Genre>>.Success(genresList);

        return Task.FromResult(result);
    }
}