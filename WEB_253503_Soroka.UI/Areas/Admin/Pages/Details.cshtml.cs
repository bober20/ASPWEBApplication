using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.UI.Services.ShowService;
using WEB_253503_Soroka.Domain.Entities;

namespace WEB_253503_Soroka.UI.Areas.Admin.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IShowService _showService;

        public DetailsModel(IShowService showService)
        {
            _showService = showService;
        }

        public Show Show { get; set; } = default!;

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
    }
}
