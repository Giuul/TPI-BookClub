using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Vote
    {
        public int Id { get; set; }
        public int valor { get; set; }

        // Relaciones
        public int UsuarioId { get; set; }
        public User Usuario { get; set; } = null!;

        public int LibroId { get; set; }
        public Book Libro { get; set; } = null!;
    }
}
