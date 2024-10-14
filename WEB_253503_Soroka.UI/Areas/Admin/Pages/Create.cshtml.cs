using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Areas.Admin.Pages.Shows
{
    public class CreateModel : PageModel
    {
        private readonly HttpClient _httpClient;
        private readonly IShowService _showService;

        public CreateModel(IShowService showService)
        {
            _showService = showService;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Show Show { get; set; } = default!;
        
        [BindProperty]
        public IFormFile ImageFile { get; set; } = default!;

        // For more information, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await _showService.CreateShowAsync(Show, ImageFile);

            return RedirectToPage("./Index");
        }
        
        
    }
}
