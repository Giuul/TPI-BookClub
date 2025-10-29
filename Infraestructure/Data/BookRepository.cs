using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Book>> GetByGeneroAsync(string genero)
        {
            return await _context.Books
                .Where(b => b.Genero.ToLower() == genero.ToLower())
                .ToListAsync();
        }

        public async Task<IEnumerable<Vote>> GetVotesByBookIdAsync(int bookId)
        {
            return await _context.Votes
                .Where(v => v.LibroId == bookId)
                .Include(v => v.Usuario)
                .ToListAsync();
        }
    }
}
