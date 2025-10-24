using Application.Models;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class VoteService : IVoteService
    {
        private readonly IVoteRepository _voteRepository;

        public VoteService(IVoteRepository voteRepository)
        {
            _voteRepository = voteRepository;
        }

        public async Task<IEnumerable<VoteDTO>> GetAllAsync()
        {
            var votes = await _voteRepository.GetAllAsync();
            return votes.Select(v => new VoteDTO        
            {
                Id = v.Id,
                Valor = v.Valor,
                UsuarioId = v.UsuarioId,
                LibroId = v.LibroId
            });
        }

        public async Task<VoteDTO?> GetByIdAsync(int id)
        {
            var vote = await _voteRepository.GetByIdAsync(id);
            if (vote == null) return null;

            return new VoteDTO
            {
                Id = vote.Id,
                Valor = vote.Valor,
                UsuarioId = vote.UsuarioId,
                LibroId = vote.LibroId
            };
        }

        public async Task<VoteDTO> CreateAsync(VoteDTO dto)
        {
            var vote = new Vote
            {
                Valor = dto.Valor,
                UsuarioId = dto.UsuarioId,
                LibroId = dto.LibroId
            };

            await _voteRepository.AddAsync(vote);
            await _voteRepository.SaveChangesAsync();

            dto.Id = vote.Id;
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var vote = await _voteRepository.GetByIdAsync(id);
            if (vote == null) return false;

            _voteRepository.Delete(vote);
            await _voteRepository.SaveChangesAsync();
            return true;
        }
    }
}

