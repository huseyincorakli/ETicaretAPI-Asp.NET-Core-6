using ETicaretAPI_V2.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : BaseEntitiy
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);
        bool RemoveRange(List<T> datas);
        Task<bool> RemoveAsync(string id);
        bool Remove(T model);
        bool Update(T model);
        Task<int> SaveAsync();
    }
}
