using Application.Interfaces;
using Application.Models;
using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VoteController : ControllerBase
    {
        private readonly IVoteService _voteService;

        public VoteController(IVoteService voteService)
        {
            _voteService = voteService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VoteDTO>>> GetAll()
        {
            var votes = await _voteService.GetAllAsync();
            return Ok(votes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<VoteDTO>> GetById(int id)
        {
            var vote = await _voteService.GetByIdAsync(id);
            if (vote == null)
                return NotFound(new { message = "Voto no encontrado." });

            return Ok(vote);
        }

        [HttpPost]
        public async Task<ActionResult<VoteDTO>> Create([FromBody] VoteDTO dto)
        {
            var created = await _voteService.CreateAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _voteService.DeleteAsync(id);
            if (!deleted)
                return NotFound(new { message = "Voto no encontrado." });

            return NoContent();
        }
    }
}