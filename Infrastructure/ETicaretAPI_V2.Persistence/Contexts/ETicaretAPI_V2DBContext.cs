using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI_V2.Persistence.Contexts
{
    public class ETicaretAPI_V2DBContext : DbContext
    { 
        //IoC DOLDURULACAK
        public ETicaretAPI_V2DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas= ChangeTracker.Entries<BaseEntitiy>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreateDate = DateTime.UtcNow,
                    EntityState.Modified=>data.Entity.UpdatedDate=DateTime.UtcNow,
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
