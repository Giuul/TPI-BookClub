using Domain.Entities;

namespace Application.Models
{
    public class BookDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Autor { get; set; } = string.Empty;
        public string Genero { get; set; } = string.Empty;
        public string? Resenia { get; set; }
        public int ListId { get; set; }

        public static BookDTO FromEntity(Book book)
            => new BookDTO
            {
                Id = book.Id,
                Titulo = book.Titulo,
                Autor = book.Autor,
                Genero = book.Genero,
                Resenia = book.Resenia,
                ListId = book.ListId
            };

        public static ICollection<BookDTO> CreateList(IEnumerable<Book> books)
            => books.Select(FromEntity).ToList();
    }
}
