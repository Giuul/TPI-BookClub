using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadingListController : ControllerBase
    {
        private readonly IReadingListService _readingListService;

        public ReadingListController(IReadingListService readingListService)
        {
            _readingListService = readingListService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReadingListDTO>>> GetAll()
        {
            var lists = await _readingListService.GetAllAsync();
            return Ok(lists);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReadingListDTO>> GetById(int id)
        {
            var list = await _readingListService.GetByIdAsync(id);
            if (list == null)
                return NotFound(new { message = "Lista no encontrada." });

            return Ok(list);
        }

        [HttpPost]
        public async Task<ActionResult<ReadingListDTO>> Create([FromBody] ReadingListDTO dto)
        {
            var created = await _readingListService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReadingListDTO>> Update(int id, [FromBody] ReadingListDTO dto)
        {
            try
            {
                var updated = await _readingListService.UpdateAsync(id, dto);
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
            var deleted = await _readingListService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Lista no encontrada." });

            return NoContent();
        }
    }
}
