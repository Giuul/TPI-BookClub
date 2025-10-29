using Microsoft.AspNetCore.Mvc;
using Application.Models;
using Application.Interfaces;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BookDTO>>> GetAll()
        {
            var books = await _bookService.GetAllAsync();
            return Ok(books);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BookDTO>> GetById(int id)
        {
            var book = await _bookService.GetByIdAsync(id);
            if (book == null)
                return NotFound(new { message = "Libro no encontrado." });

            return Ok(book);
        }

        [HttpPost]
        public async Task<ActionResult<BookDTO>> Create([FromBody] BookDTO dto)
        {
            var created = await _bookService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BookDTO>> Update(int id, [FromBody] BookDTO dto)
        {
            try
            {
                var updated = await _bookService.UpdateAsync(id, dto);
                return Ok(updated);
            }
            catch (Exception ex)
            {
                return NotFound(new { message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _bookService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Libro no encontrado." });

            return NoContent();
        }
    }
}