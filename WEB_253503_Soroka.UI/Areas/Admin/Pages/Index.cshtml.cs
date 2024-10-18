using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.UI.Services.ShowService;

namespace WEB_253503_Soroka.UI.Areas.Admin.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IShowService _showService;

        public IndexModel(IShowService showService)
        {
            _showService = showService;
        }

        public IList<Show> Show { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Show = (await _showService.GetShowListAsync()).Data.Items;
        }
    }
}
