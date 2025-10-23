using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class ReadingListRepository : IReadingListRepository
    {
        private readonly ApplicationDbContext _context;

        public ReadingListRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ReadingList>> GetAllAsync()
        {
            return await _context.ReadingLists
                .Include(l => l.Libros)
                .Include(l => l.Creador)
                .ToListAsync();
        }

        public async Task<ReadingList?> GetByIdAsync(int id)
        {
            return await _context.ReadingLists
                .Include(l => l.Libros)
                .Include(l => l.Creador)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task AddAsync(ReadingList list)
        {
            await _context.ReadingLists.AddAsync(list);
        }

        public void Update(ReadingList list)
        {
            _context.ReadingLists.Update(list);
        }

        public void Delete(ReadingList list)
        {
            _context.ReadingLists.Remove(list);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

