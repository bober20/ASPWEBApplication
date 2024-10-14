using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Areas.Admin.Pages
{
    public class EditModel : PageModel
    {
        private readonly IShowService _showService;
        
        public EditModel(IShowService showService)
        {
            _showService = showService;
        }
        
        [BindProperty]
        public Show Show { get; set; } = default!;
        
        [BindProperty]
        public IFormFile? ImageFile { get; set; } = default!;
        
        public async Task<IActionResult> OnGetAsync(int id)
        {
            var response = await _showService.GetShowByIdAsync(id);

            if (response.Successfull)
            {
                Show = response.Data;
                return Page();
            }
            
            return NotFound();
        }
        
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _showService.UpdateShowAsync(Show.Id, Show, ImageFile);
        
            return RedirectToPage("./Index");
        }
        
        private async Task<bool> ShowExists(int id)
        {
            return (await _showService.GetShowByIdAsync(id)).Successfull;
        }
    }
}
