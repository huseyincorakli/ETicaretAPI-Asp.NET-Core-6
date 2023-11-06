using ETicaretAPI_V2.Domain.Entities;
using ETicaretAPI_V2.Domain.Entities.Common;
using ETicaretAPI_V2.Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETicaretAPI_V2.Persistence.Contexts
{
    public class ETicaretAPI_V2DBContext : IdentityDbContext<AppUser, AppRole, string>
    {
        //IoC DOLDURULACAK
        public ETicaretAPI_V2DBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
       // public DbSet<ProductTag> ProductTags { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
       // public DbSet<Customer> Customers { get; set; }
        public DbSet<Domain.Entities.File> Files { get; set; }
        public DbSet<ProductImageFile> ProductImageFiles { get; set; }
        public DbSet<InvoiceFile> InvoiceFiles { get; set; }
        public DbSet<Basket> Baskets { get; set; }
        public DbSet<BasketItem> BasketItems { get; set; }
        public DbSet<CompletedOrder> CompletedOrders { get; set; }
        public DbSet<Menu> Menus { get; set; }
        public DbSet<Endpoint> Endpoints { get; set; }
        public DbSet<Address> Addresses { get; set; }


		protected override void OnModelCreating(ModelBuilder builder)
        {
            

            builder.Entity<Order>()
               .HasKey(b => b.Id);
            builder.Entity<Order>()
                .HasIndex(o => o.OrderCode)
                .IsUnique();
            builder.Entity<Basket>()
                .HasOne(b => b.Order)
                .WithOne(o => o.Basket)
                .HasForeignKey<Order>(b => b.Id);

            builder.Entity<Order>()
                 .HasOne(o => o.CompletedOrder)
                 .WithOne(co => co.Order)
                 .HasForeignKey<CompletedOrder>(co=>co.OrderId);

            builder.Entity<AppUser>()
                .HasOne(au => au.Address)
                .WithOne(ad => ad.AppUser)
                .HasForeignKey<Address>(ad => ad.UserId);

            base.OnModelCreating(builder);
            base.OnModelCreating(builder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntitiy>();
            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreateDate = DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow,
                    _ => DateTime.UtcNow
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
