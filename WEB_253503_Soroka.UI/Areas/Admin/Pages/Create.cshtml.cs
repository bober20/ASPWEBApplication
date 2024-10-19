using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.UI.Services.GenreService;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Areas.Admin.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IShowService _showService;
        private readonly IGenreService _genreService;

        public CreateModel(IShowService showService, IGenreService genreService)
        {
            _showService = showService;
            _genreService = genreService;
        }

        public async Task<IActionResult> OnGet()
        {
            var response = await _genreService.GetGenreListAsync();

            if (response.Successfull)
            {
                Genres = response.Data;
            }
            
            return Page();
        }
        
        [BindProperty]
        public List<Genre> Genres { get; set; } = default!;

        [BindProperty]
        public Show Show { get; set; } = default!;
        
        [BindProperty]
        public IFormFile? ImageFile { get; set; }
        
        [BindProperty]
        public int ChosenGenreId { get; set; } = default!;
        
        public async Task<IActionResult> OnPostAsync()
        {
            var response = await _genreService.GetGenreListAsync();
            var genres = new List<Genre>();

            if (response.Successfull)
            {
                genres = response.Data;
            }
            
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Show.Genre = genres.Find(genre => genre.Id == ChosenGenreId);

            await _showService.CreateShowAsync(Show, ImageFile);

            return RedirectToPage("./Index");
        }
    }
}
