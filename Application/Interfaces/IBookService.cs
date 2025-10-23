using Application.DTOs;

namespace Application.Interfaces
{
    public interface IBookService
    {
        Task<IEnumerable<BookDTO>> GetAllAsync();
        Task<BookDTO?> GetByIdAsync(int id);
        Task<BookDTO> CreateAsync(BookDTO dto);
        Task<BookDTO> UpdateAsync(int id, BookDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}

