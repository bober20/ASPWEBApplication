// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.EntityFrameworkCore;
// using WEB_253503_Soroka.API.Data;
// using WEB_253503_Soroka.Domain.Entities;
// using WEB_253503_Soroka.UI.Services.ShowService;
//
// namespace WEB_253503_Soroka.UI.Areas.Admin.Pages.Shows
// {
//     public class DeleteModel : PageModel
//     {
//         private readonly IShowService _showService;
//
//         public DeleteModel(IShowService showService)
//         {
//             _showService = showService;
//         }
//
//         [BindProperty]
//         public Show Show { get; set; } = default!;
//         
//         [BindProperty]
//         public IFormFile ImageFile { get; set; } = default!;
//
//         public async Task<IActionResult> OnGetAsync(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var shows = _showService.GetShowListAsync(id);
//
//             if (show == null)
//             {
//                 return NotFound();
//             }
//             else
//             {
//                 Show = show;
//             }
//             return Page();
//         }
//
//         public async Task<IActionResult> OnPostAsync(int? id)
//         {
//             if (id == null)
//             {
//                 return NotFound();
//             }
//
//             var show = await _context.Shows.FindAsync(id);
//             if (show != null)
//             {
//                 Show = show;
//                 _context.Shows.Remove(Show);
//                 await _context.SaveChangesAsync();
//             }
//
//             return RedirectToPage("./Index");
//         }
//     }
// }
