using Domain.Entities;
using Domain.Interfaces;
using Infraestructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure.Repositories
{
    public class VoteRepository : IVoteRepository
    {
        private readonly ApplicationDbContext _context;

        public VoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Vote>> GetAllAsync()
        {
            return await _context.Votes
                .Include(v => v.Usuario)
                .Include(v => v.Libro)
                .ToListAsync();
        }

        public async Task<Vote?> GetByIdAsync(int id)
        {
            return await _context.Votes
                .Include(v => v.Usuario)
                .Include(v => v.Libro)
                .FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task AddAsync(Vote vote)
        {
            await _context.Votes.AddAsync(vote);
        }

        public void Delete(Vote vote)
        {
            _context.Votes.Remove(vote);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

