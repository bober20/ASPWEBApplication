using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        // GET: api/Shows
        // [HttpGet]
        // public async Task<ActionResult<IEnumerable<Show>>> GetShows()
        // {
        //     return await _context.Shows.ToListAsync();
        // }
        
        // [HttpGet]
        // public async Task<ActionResult<ResponseData<ListModel<Show>>>> GetShows()
        // {
        //     return Ok(await _service.GetShowListAsync());
        // }
        
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
        public async Task<IActionResult> PutShow(int id, Show show)
        {
            if (id != show.Id)
            {
                return BadRequest();
            }

            _context.Entry(show).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ShowExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Shows
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // [HttpPost]
        // public async Task<ActionResult<Show>> PostShow(Show show)
        // {
        //     _context.Shows.Add(show);
        //     await _context.SaveChangesAsync();
        //
        //     return CreatedAtAction("GetShow", new { id = show.Id }, show);
        // }

        // DELETE: api/Shows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteShow(int id)
        {
            var show = await _context.Shows.FindAsync(id);
            if (show == null)
            {
                return NotFound();
            }

            _context.Shows.Remove(show);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ShowExists(int id)
        {
            return _context.Shows.Any(e => e.Id == id);
        }
    }
}
