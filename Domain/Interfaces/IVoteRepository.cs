using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IVoteRepository
    {
        Task<IEnumerable<Vote>> GetAllAsync();
        Task<Vote?> GetByIdAsync(int id);
        Task AddAsync(Vote vote);
        void Delete(Vote vote);
        Task SaveChangesAsync();
    }
}
