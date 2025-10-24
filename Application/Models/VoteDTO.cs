using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models
{
    public class VoteDTO
    {
        public int Id { get; set; }
        public int Valor { get; set; }
        public int UsuarioId { get; set; }
        public int LibroId { get; set; }
    }
}
