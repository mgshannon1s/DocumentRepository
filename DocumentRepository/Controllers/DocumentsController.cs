using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocumentRepository.Models;
using System.IO;

namespace DocumentRepository.Controllers
{
    [Route("api/documents")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentContext _context;

        public DocumentsController(DocumentContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Document>>> GetTodoItems()
        {
            return await _context.TodoItems.ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(long id)
        {
            var document = await _context.TodoItems.FindAsync(id);

            if (document == null)
            {
                return NotFound();
            }

            return document;
        }

        // PUT: api/Documents/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocument(long id, Document document)
        {
            if (id != document.Id)
            {
                return BadRequest();
            }

            _context.Entry(document).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentExists(id))
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

        // POST: api/Documents
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(IFormFile file)
        {
            //Create new Document with information from file passed in.
            //TODO: don't trust filename from the input.
            Document document = new Document() 
            { 
                Name = file.FileName
            };
            MemoryStream memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            document.Contents = memoryStream.ToArray();

            _context.TodoItems.Add(document);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDocument), new { id = document.Id }, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Document>> DeleteDocument(long id)
        {
            var document = await _context.TodoItems.FindAsync(id);
            if (document == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(document);
            await _context.SaveChangesAsync();

            return document;
        }

        private bool DocumentExists(long id)
        {
            return _context.TodoItems.Any(e => e.Id == id);
        }
    }
}
