using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Areas.Admin.Pages
{
    public class DeleteModel : PageModel
    {
        private readonly IShowService _showService;

        public DeleteModel(IShowService showService)
        {
            _showService = showService;
        }

        [BindProperty]
        public Show Show { get; set; } = default!;
        
        [BindProperty]
        public IFormFile ImageFile { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var response = await _showService.GetShowByIdAsync((int)id);

            if (response.Successfull)
            {
                Show = response.Data;
                
                return Page();
            }
            
            return NotFound();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            await _showService.DeleteShowAsync((int)id);
            return RedirectToPage("./Index");
        }
    }
}
