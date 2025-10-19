using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infraestructure.Data
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        private readonly DbContext _context;

        protected RepositoryBase(DbContext context)
        {
            _context = context;
        }
    }
}
