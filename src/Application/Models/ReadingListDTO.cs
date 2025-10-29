using Domain.Entities;

namespace Application.Models
{
    public class ReadingListDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string? Descripcion { get; set; }
        public bool EsCompartida { get; set; }
        public int CreadorId { get; set; }

        public static ReadingListDTO FromEntity(ReadingList list)
            => new ReadingListDTO
            {
                Id = list.Id,
                Titulo = list.Titulo,
                Descripcion = list.Descripcion,
                EsCompartida = list.EsCompartida,
                CreadorId = list.CreadorId
            };

        public static ICollection<ReadingListDTO> CreateList(IEnumerable<ReadingList> lists)
            => lists.Select(FromEntity).ToList();
    }
}

