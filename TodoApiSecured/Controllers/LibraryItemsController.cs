using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryApi.Models;
using LibraryApi.Attributes;
using System;

namespace LibraryApi.Controllers
{
    [ApiKey]
    [Route("api/[controller]")]
    [ApiController]    
    public class LibraryItemsController : ControllerBase
    {
        private readonly LibraryContext _context;

        public LibraryItemsController(LibraryContext context)
        {
            _context = context;
        }



        // GET: api/LibraryItems   notice: we don't use method name
        [HttpGet]
        [Produces("application/json")]
        [SwaggerOperation("Zwraca wszystkie produkty z listy.")]
        [SwaggerResponse(200, "Sukces", Type = typeof(List<LibraryItem>))]        
        public async Task<ActionResult<IEnumerable<LibraryItem>>> GetLibraryItems()
        {
            return await _context.LibraryItems.ToListAsync();  //http 200
        }



        // GET: api/LibraryItems/5
        [HttpGet("{id}")]        
        [Produces("application/json")]
        [SwaggerOperation("Zwraca produkt o podanym {id}.")]
        [SwaggerResponse(200, "Znaleziono produktu o podanym {id}", Type = typeof(LibraryItem))]
        [SwaggerResponse(404, "Nie znaleziono produktu o podanym {id}")]
        public async Task<ActionResult<LibraryItem>> GetLibraryItem(
            [SwaggerParameter("Podaj id prodduktu który chcesz odczytać", Required = true)]
            int id)
        {
            var LibraryItem = await _context.LibraryItems.FindAsync(id);

            if (LibraryItem == null)
            {
                return NotFound(); //http 404
            }

            return LibraryItem;    //http 200
        }


        // PUT: api/LibraryItems/5        
        [HttpPut("{id}")]
        [Produces("application/json")]
        [SwaggerOperation("Aktualizuje zadanie o podanym {id}.", "Używa EF")]
        [SwaggerResponse(204, "Zaktualizowano zadanie o podanym {id}")]
        [SwaggerResponse(400, "Nie rozpoznano danych wejściowych")]
        [SwaggerResponse(404, "Nie znaleziono zadania o podanym {id}")]
        public async Task<IActionResult> PutLibraryItem(
            [SwaggerParameter("Podaj nr zadnia które chcesz zaktualizować", Required = true)]
            int id,
            [SwaggerParameter("Definicja zadania", Required = true)]
            LibraryItem LibraryItem)
        {
            if (id != LibraryItem.Id)
            {
                return BadRequest(); //http 400
            }

            _context.Entry(LibraryItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LibraryItemExists(id))
                {
                    return NotFound();  //http 404
                }
                else
                {
                    throw;
                }
            }

            return NoContent(); //http 204
        }


        // POST: api/LibraryItems        
        [HttpPost]
        [Produces("application/json")]
        [SwaggerOperation("Tworzy nowe zadanie.", "Używa EF")]
        [SwaggerResponse(201, "Zapis z sukcesem.", Type = typeof(LibraryItem))]
        public async Task<ActionResult<LibraryItem>> PostLibraryItem(
            [SwaggerParameter("Definicja zadania", Required = true)]
           LibraryItem LibraryItem)
        {
            _context.LibraryItems.Add(LibraryItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLibraryItem", new { id = LibraryItem.Id }, LibraryItem);  //http 201, add Location header
        }



        // DELETE: api/LibraryItems/5
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [SwaggerOperation("Usuwa zadanie o podanym {id}.", "Używa EF")]
        [SwaggerResponse(204, "Usunięto zadanie o podanym {id}")]        
        [SwaggerResponse(404, "Nie znaleziono zadania o podanym {id}")]
        public async Task<IActionResult> DeleteLibraryItem(
            [SwaggerParameter("Podaj nr zadnia które chcesz usunąć", Required = true)]
            int id)
        {
            var LibraryItem = await _context.LibraryItems.FindAsync(id);
            if (LibraryItem == null)
            {
                return NotFound();  //http 404
            }

            _context.LibraryItems.Remove(LibraryItem);
            await _context.SaveChangesAsync();

            return NoContent(); //http 204
        }



        private bool LibraryItemExists(int id)
        {
            return _context.LibraryItems.Any(e => e.Id == id);
        }


    }
}
