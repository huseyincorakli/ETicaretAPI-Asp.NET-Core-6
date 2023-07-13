using ETicaretAPI_V2.Application.Repositories;
using ETicaretAPI_V2.Domain.Entities.Common;
using ETicaretAPI_V2.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Persistence.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : BaseEntitiy
    {
        private readonly ETicaretAPI_V2DBContext _context;

        public ReadRepository(ETicaretAPI_V2DBContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public IQueryable<T> GetAll(bool tracking = true)
        {
            var query= Table.AsQueryable();
            if(!tracking)
                query=Table.AsNoTracking();
            return query;

        }

        public async Task<T> GetByIdAsync(string id, bool tracking = true)
        //Mark Pattern => await Table.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
        //=> await Table.FindAsync(Guid.Parse(id));
        {
            var query=  Table.AsQueryable();
            if(!tracking)
                query = Table.AsNoTracking();
            return  await query.FirstOrDefaultAsync(p => p.Id == Guid.Parse(id));
        }

        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query= Table.AsQueryable();
            if(!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }

        public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.Where(method);
            if(!tracking)
                query = Table.AsNoTracking();
            return query;
        }


    }
}
