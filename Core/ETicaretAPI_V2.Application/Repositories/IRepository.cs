using ETicaretAPI_V2.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Application.Repositories
{
    public interface IRepository<T> where T : BaseEntitiy
    {
        DbSet<T> Table { get; }
    }
}
