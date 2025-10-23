using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IReadingListRepository
    {
        Task<IEnumerable<ReadingList>> GetAllAsync();
        Task<ReadingList?> GetByIdAsync(int id);
        Task AddAsync(ReadingList list);
        void Update(ReadingList list);
        void Delete(ReadingList list);
        Task SaveChangesAsync();
    }
}
