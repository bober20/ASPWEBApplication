using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.Domain.Entities;

namespace WEB_253503_Soroka.API.Data;

public class DbInitializer
{
    public static async Task SeedData(WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

            // Применение миграций перед инициализацией данных
            await dbContext.Database.MigrateAsync();

            var imageUrl = app.Configuration.GetValue<string>("ApiProjectUrl");

            var genresList = CreateGenreList();
            var showsList = CreateShowList(genresList, imageUrl!);

            // Проверка на существование данных
            if (!dbContext.Genres.Any())
            {
                await dbContext.Genres.AddRangeAsync(genresList);
                await dbContext.SaveChangesAsync();
            }

            if (!dbContext.Shows.Any())
            {
                await dbContext.Shows.AddRangeAsync(showsList);
                await dbContext.SaveChangesAsync();
            }
        }
    }
    
    // public static async Task SeedData(WebApplication app)
    // {
    //     var scope = app.Services.CreateScope();
    //     var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    //
    //     var imageUrl = app.Configuration.GetValue<string>("ApiProjectUrl");
    //
    //     var genresList = CreateGenreList();
    //     var showsList = CreateShowList(genresList, imageUrl!);
    //
    //     foreach (var genre in genresList)
    //     {
    //         await dbContext.AddAsync(genre).AsTask();
    //     }
    //     
    //     await dbContext.SaveChangesAsync();
    //     
    //     foreach (var show in showsList)
    //     {
    //         await dbContext.AddAsync(show).AsTask();
    //     }
    //
    //     await dbContext.SaveChangesAsync();
    // }

    private static List<Genre> CreateGenreList()
    {
        var genresList = new List<Genre>
        {
            new Genre
            {
                Name = "Ужасы",
                NormalizedName = "horror",
            },
            new Genre
            {
                Name = "Триллер",
                NormalizedName = "thriller",
            },
            new Genre
            {
                Name = "Драма",
                NormalizedName = "drama",
            },
            new Genre
            {
                Name = "Фантастика",
                NormalizedName = "fantasy",
            },
            new Genre
            {
                Name = "Комедия",
                NormalizedName = "comedy",
            },
            new Genre
            {
                Name = "Приключения",
                NormalizedName = "adventure",
            }
        };

        return genresList;
    }

    private static List<Show> CreateShowList(List<Genre> genreList, string imageUrl)
    {
        var showsList = new List<Show>
        {
            new Show
            {
                Name = "Охотник за разумом",
                Description = "Конец 1970-х. Два агента ФБР опрашивают находящихся в заключении серийных убийц с целью понимания их образа мыслей, а также раскрытия текущих преступлений.",
                Genre = genreList.Find((genre => genre.NormalizedName.Equals("thriller"))),
                Price = 200,
                Image = imageUrl + "/Images/mindhunter.webp",
            },
            new Show
            {
                Name = "Джентельмены",
                Description = "Молодой человек по\u00a0имени Эдди Холстед узнаёт, что полученное им большое наследство связано с наркоимперией Бобби Гласса.",
                Genre = genreList.Find((genre => genre.NormalizedName.Equals("comedy"))),
                Price = 180,
                Image = imageUrl + "/Images/gentlemen.webp",
            },
            new Show
            {
                Name = "Табу",
                Description = "Дерзкий авантюрист против Англии, США и Ост-Индской компании.",
                Genre = genreList.Find((genre => genre.NormalizedName.Equals("drama"))),
                Price = 220,
                Image = imageUrl + "/Images/taboo.webp",
            },
            new Show
            {
                Name = "Падение дома Ашеров",
                Description = "В семье Ашеров загадочным образом умирают все наследники.",
                Genre = genreList.Find((genre => genre.NormalizedName.Equals("horror"))),
                Price = 230,
                Image = imageUrl + "/Images/the_fall_of_the_house_of_usher.webp",
            },
            new Show
            {
                Name = "Больница Никербокер",
                Description = "Гениальный хирург против системы и наркозависимости.",
                Genre = genreList.Find((genre => genre.NormalizedName.Equals("thriller"))),
                Price = 230,
                Image = imageUrl + "/Images/the_knick.webp",
            },
            new Show
            {
                Name = "История девятихвостого лиса",
                Description = "Оборотень находит и реинкарнацию своей первой любви, и древнего врага.",
                Genre = genreList.Find((genre => genre.NormalizedName.Equals("fantasy"))),
                Price = 230,
                Image = imageUrl + "/Images/gumihodyeon.webp",
            },
            new Show
            {
                Name = "Уроки химии",
                Description = "В 1950-х годах мечта одной женщины стать ученым сталкивается с общественным мнением, согласно которому место женщин — только в домашней сфере.",
                Genre = genreList.Find((genre => genre.NormalizedName.Equals("drama"))),
                Price = 230,
                Image = imageUrl + "/Images/lessons_in_chemistry.webp",
            }
        };

        return showsList;
    }
}