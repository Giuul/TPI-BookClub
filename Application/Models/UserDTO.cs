﻿using Domain.Entities;

namespace Application.Models
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Rol { get; set; } = "usuario";
        public string? Password { get; set; }

        public static UserDTO FromEntity(User user)
            => new UserDTO
            {
                Id = user.Id,
                Nombre = user.Nombre,
                Email = user.Email,
                Rol = user.Rol.ToString()
            };

        public static ICollection<UserDTO> CreateList(IEnumerable<User> users)
            => users.Select(FromEntity).ToList();
    }
}
