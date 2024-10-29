using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.UI.Services.GenreService;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly IShowService _showService;
        private readonly IGenreService _genreService;
        
        public EditModel(IShowService showService, IGenreService genreService)
        {
            _showService = showService;
            _genreService = genreService;
        }
        
        [BindProperty]
        public Show Show { get; set; } = default!;
        
        [BindProperty]
        public IFormFile? ImageFile { get; set; } = default!;
        
        [BindProperty]
        public List<Genre> Genres { get; set; } = default!;
        
        [BindProperty]
        public int ChosenGenreId { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _genreService.GetGenreListAsync();

            if (response.Successfull)
            {
                Genres = response.Data;
            }
            
            var responseShows = await _showService.GetShowByIdAsync(id);

            if (responseShows.Successfull)
            {
                Show = responseShows.Data;
                return Page();
            }
            
            return NotFound();
        }
        
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
            
            await _showService.UpdateShowAsync(Show.Id, Show, ImageFile);
        
            return RedirectToPage("./Index");
        }
        
        private async Task<bool> ShowExists(int id)
        {
            return (await _showService.GetShowByIdAsync(id)).Successfull;
        }
    }
}
