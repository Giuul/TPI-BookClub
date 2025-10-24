using Application.Models;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class ReadingListService : IReadingListService
    {
        private readonly IReadingListRepository _listRepository;

        public ReadingListService(IReadingListRepository listRepository)
        {
            _listRepository = listRepository;
        }

        public async Task<IEnumerable<ReadingListDTO>> GetAllAsync()
        {
            var lists = await _listRepository.GetAllAsync();
            return lists.Select(l => new ReadingListDTO
            {
                Id = l.Id,
                Titulo = l.Titulo,
                Descripcion = l.Descripcion,
                EsCompartida = l.EsCompartida,
                CreadorId = l.CreadorId
            });
        }

        public async Task<ReadingListDTO?> GetByIdAsync(int id)
        {
            var list = await _listRepository.GetByIdAsync(id);
            if (list == null) return null;

            return new ReadingListDTO
            {
                Id = list.Id,
                Titulo = list.Titulo,
                Descripcion = list.Descripcion,
                EsCompartida = list.EsCompartida,
                CreadorId = list.CreadorId
            };
        }

        public async Task<ReadingListDTO> CreateAsync(ReadingListDTO dto)
        {
            var list = new ReadingList
            {
                Titulo = dto.Titulo,
                Descripcion = dto.Descripcion,
                EsCompartida = dto.EsCompartida,
                CreadorId = dto.CreadorId
            };

            await _listRepository.AddAsync(list);
            await _listRepository.SaveChangesAsync();

            dto.Id = list.Id;
            return dto;
        }

        public async Task<ReadingListDTO> UpdateAsync(int id, ReadingListDTO dto)
        {
            var list = await _listRepository.GetByIdAsync(id);
            if (list == null) throw new Exception("Lista no encontrada");

            list.Titulo = dto.Titulo;
            list.Descripcion = dto.Descripcion;
            list.EsCompartida = dto.EsCompartida;
            list.CreadorId = dto.CreadorId;

            _listRepository.Update(list);
            await _listRepository.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var list = await _listRepository.GetByIdAsync(id);
            if (list == null) return false;

            _listRepository.Delete(list);
            await _listRepository.SaveChangesAsync();
            return true;
        }
    }
}
