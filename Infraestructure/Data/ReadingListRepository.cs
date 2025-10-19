using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class ReadingListRepository : RepositoryBase<ReadingList> , IReadingListRepository
    {
        private readonly ApplicationDbContext _context;

        public ReadingListRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
