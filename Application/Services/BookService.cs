using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDTO>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            return books.Select(b => new BookDTO
            {
                Id = b.Id,
                Titulo = b.Titulo,
                Autor = b.Autor,
                Genero = b.Genero,
                Resenia = b.Resenia,
                ListId = b.ListId
            });
        }

        public async Task<BookDTO?> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return null;

            return new BookDTO
            {
                Id = book.Id,
                Titulo = book.Titulo,
                Autor = book.Autor,
                Genero = book.Genero,
                Resenia = book.Resenia,
                ListId = book.ListId
            };
        }

        public async Task<BookDTO> CreateAsync(BookDTO dto)
        {
            var book = new Book
            {
                Titulo = dto.Titulo,
                Autor = dto.Autor,
                Genero = dto.Genero,
                Resenia = dto.Resenia,
                ListId = dto.ListId
            };

            await _bookRepository.AddAsync(book);
            await _bookRepository.SaveChangesAsync();

            dto.Id = book.Id;
            return dto;
        }

        public async Task<BookDTO> UpdateAsync(int id, BookDTO dto)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) throw new Exception("Libro no encontrado.");

            book.Titulo = dto.Titulo;
            book.Autor = dto.Autor;
            book.Genero = dto.Genero;
            book.Resenia = dto.Resenia;
            book.ListId = dto.ListId;

            _bookRepository.Update(book);
            await _bookRepository.SaveChangesAsync();

            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            if (book == null) return false;

            _bookRepository.Delete(book);
            await _bookRepository.SaveChangesAsync();
            return true;
        }
    }
}
