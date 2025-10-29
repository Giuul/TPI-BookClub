using Domain.Entities;

namespace Application.Models
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }

        public static VoteDTO FromEntity(Vote vote)
            => new VoteDTO
            {
                Id = vote.Id,
                Valor = vote.Valor,
                UsuarioId = vote.UsuarioId,
                LibroId = vote.LibroId
            };

        public static ICollection<VoteDTO> CreateList(IEnumerable<Vote> votes)
            => votes.Select(FromEntity).ToList();
    }
}
