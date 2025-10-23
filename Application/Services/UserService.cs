using Application.DTOs;
using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<UserDTO>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDTO
            {
                Id = u.Id,
                Nombre = u.Nombre,
                Email = u.Email,
                Password = u.Password,
                Rol = u.Rol.ToString()
            });
        }

        public async Task<UserDTO?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Password = user.Password,
                Rol = user.Rol.ToString()
            };
        }

        public async Task<UserDTO> RegisterAsync(UserDTO dto)
        {
            var user = new User
            {
                Nombre = dto.Nombre,
                Email = dto.Email,
                Password = dto.Password,
                Rol = Enum.TryParse<Rol>(dto.Rol, out var rol) ? rol : Rol.usuario
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();

            dto.Id = user.Id;
            return dto;
        }

        public async Task<UserDTO> UpdateAsync(int id, UserDTO dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) throw new Exception("Usuario no encontrado");

            user.Nombre = dto.Nombre;
            user.Email = dto.Email;
            user.Password = dto.Password;
            user.Rol = Enum.TryParse<Rol>(dto.Rol, out var rol) ? rol : Rol.usuario;

            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return dto;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }
    }
}
