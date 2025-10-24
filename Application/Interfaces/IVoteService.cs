using Application.Models;

namespace Application.Interfaces
{
    public interface IVoteService
    {
        Task<IEnumerable<VoteDTO>> GetAllAsync();
        Task<VoteDTO?> GetByIdAsync(int id);
        Task<VoteDTO> CreateAsync(VoteDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}
