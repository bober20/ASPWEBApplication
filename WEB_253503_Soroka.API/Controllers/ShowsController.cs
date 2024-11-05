using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WEB_253503_Soroka.API.Data;
using WEB_253503_Soroka.API.Services.ShowService;
using WEB_253503_Soroka.Domain.Entities;
using WEB_253503_Soroka.Domain.Models;

namespace WEB_253503_Soroka.API.Controllers
{
    [Route("api/shows")]
    [ApiController]
    public class ShowsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IShowService _service;

        public ShowsController(AppDbContext context, IShowService service)
        {
            _context = context;
            _service = service;
        }
        
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Show>>>> GetShows()
        {
            return Ok(await _service.GetShowListAsync());
        }
        
        [Route("genres/{genreNormalizedName?}")]
        [HttpGet]
        public async Task<ActionResult<ResponseData<ListModel<Show>>>> GetShows(string? genreNormalizedName, 
                                                                                int pageNo = 1, 
                                                                                int pageSize = 3)
        {
            return Ok(await _service.GetShowListAsync(genreNormalizedName, pageNo, pageSize));
        }

        // GET: api/Shows/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Show>> GetShow(int id)
        {
            return Ok(await _service.GetShowByIdAsync(id));
        }

        // PUT: api/Shows/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> PutShow(int id, Show show)
        {
            if (id != show.Id)
            {
                return BadRequest();
            }
        
            await _service.UpdateShowAsync(id, show);
        
            return NoContent();
        }

        [HttpPost]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> PostShow(Show show)
        {
            return Ok(await _service.CreateShowAsync(show));
        }

        // DELETE: api/Shows/5
        [HttpDelete("{id}")]
        [Authorize(Policy = "admin")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            var response = await _service.GetShowByIdAsync(id);
            
            if (!response.Successfull)
            {
                return NotFound();
            }

            await _service.DeleteShowAsync(id);

            return NoContent();
        }

        private bool ShowExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }
    }
}
