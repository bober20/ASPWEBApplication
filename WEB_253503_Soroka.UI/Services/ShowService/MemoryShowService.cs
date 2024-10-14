// using Microsoft.AspNetCore.Mvc;
// using WEB_253503_Soroka.Domain.Entities;
// using WEB_253503_Soroka.Domain.Models;
// using WEB_253503_Soroka.UI.Services.GenreService;
//
// namespace WEB_253503_Soroka.UI.Services.ShowService;
//
// public class MemoryShowService : IShowService
// {
//     private List<Show> _shows;
//     private readonly List<Genre> _genres;
//
//     private IConfiguration _configuration;
//
//     public MemoryShowService(IGenreService genreService, [FromServices] IConfiguration configuration)
//     {
//         _genres = genreService.GetGenreListAsync().Result.Data;
//         _configuration = configuration;
//
//         SetupData();
//     }
//
//     private void SetupData()
//     {
//         _shows = new List<Show>
//         {
//             new Show
//             {
//                 Id = 1,
//                 Name = "Охотник за разумом",
//                 Description = "Конец 1970-х. Два агента ФБР опрашивают находящихся в заключении серийных убийц с целью понимания их образа мыслей, а также раскрытия текущих преступлений.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("thriller"))),
//                 Price = 200,
//                 Image = "Images/mindhunter.webp",
//             },
//             new Show
//             {
//                 Id = 2,
//                 Name = "Джентельмены",
//                 Description = "Молодой человек по\u00a0имени Эдди Холстед узнаёт, что полученное им большое наследство связано с наркоимперией Бобби Гласса.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("comedy"))),
//                 Price = 180,
//                 Image = "Images/gentlemen.webp",
//             },
//             new Show
//             {
//                 Id = 3,
//                 Name = "Табу",
//                 Description = "Дерзкий авантюрист против Англии, США и Ост-Индской компании.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("drama"))),
//                 Price = 220,
//                 Image = "Images/taboo.webp",
//             },
//             new Show
//             {
//                 Id = 4,
//                 Name = "Падение дома Ашеров",
//                 Description = "В семье Ашеров загадочным образом умирают все наследники.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("horror"))),
//                 Price = 230,
//                 Image = "Images/the_fall_of_the_house_of_usher.webp",
//             },
//             new Show
//             {
//                 Id = 5,
//                 Name = "Больница Никербокер",
//                 Description = "Гениальный хирург против системы и наркозависимости.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("thriller"))),
//                 Price = 230,
//                 Image = "Images/the_knick.webp",
//             },
//             new Show
//             {
//                 Id = 6,
//                 Name = "История девятихвостого лиса",
//                 Description = "Оборотень находит и реинкарнацию своей первой любви, и древнего врага.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("fantasy"))),
//                 Price = 230,
//                 Image = "Images/gumihodyeon.webp",
//             },
//             new Show
//             {
//                 Id = 7,
//                 Name = "Уроки химии",
//                 Description = "В 1950-х годах мечта одной женщины стать ученым сталкивается с общественным мнением, согласно которому место женщин — только в домашней сфере.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("drama"))),
//                 Price = 230,
//                 Image = "Images/lessons_in_chemistry.webp",
//             },
//             new Show
//             {
//                 Id = 8,
//                 Name = "Больница Никербокер",
//                 Description = "Гениальный хирург против системы и наркозависимости.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("thriller"))),
//                 Price = 230,
//                 Image = "Images/the_knick.webp",
//             },
//             new Show
//             {
//                 Id = 9,
//                 Name = "Больница Никербокер",
//                 Description = "Гениальный хирург против системы и наркозависимости.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("thriller"))),
//                 Price = 230,
//                 Image = "Images/the_knick.webp",
//             },
//             new Show
//             {
//                 Id = 10,
//                 Name = "Больница Никербокер",
//                 Description = "Гениальный хирург против системы и наркозависимости.",
//                 Genre = _genres.Find((genre => genre.NormalizedName.Equals("thriller"))),
//                 Price = 230,
//                 Image = "Images/the_knick.webp",
//             },
//         };
//     }
//     
//     public Task<ResponseData<ListModel<Show>>> GetShowListAsync(string? genreNormalizedName, int pageNo = 1)
//     {
//         var shows = _shows.Where(show => genreNormalizedName == null || show.Genre.NormalizedName == genreNormalizedName);
//         var itemsPerPage = _configuration.GetValue<int>("ItemsPerPage");
//         ListModel<Show> showListModel = new()
//         {
//             Items = shows.Skip((pageNo - 1) * itemsPerPage).Take(itemsPerPage).ToList(),
//             CurrentPage = pageNo,
//             TotalPages = (int)Math.Ceiling((double)shows.Count() / itemsPerPage)
//         };
//         var result = ResponseData<ListModel<Show>>.Success(showListModel);
//         return Task.FromResult(result);
//     }
//     
//     public Task<ResponseData<List<Show>>> GetShowListAsync()
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<ResponseData<Show>> GetShowByIdAsync(int id)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task UpdateShowAsync(int id, Show show, IFormFile? formFile)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task DeleteShowAsync(int id)
//     {
//         throw new NotImplementedException();
//     }
//
//     public Task<ResponseData<Show>> CreateShowAsync(Show show, IFormFile? formFile)
//     {
//         throw new NotImplementedException();
//     }
// }