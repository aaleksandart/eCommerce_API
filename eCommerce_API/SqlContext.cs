using eCommerce_API.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace eCommerce_API
{
    public class SqlContext : DbContext
    {
        protected SqlContext()
        {

        }

        public SqlContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<AddressEntity> Addresses { get; set; }
        public virtual DbSet<CategoryEntity> Categories { get; set; }
        public virtual DbSet<ContactInfoEntity> ContactInfo { get; set; }
        public virtual DbSet<ProductEntity> Products { get; set; }
        public virtual DbSet<OrderEntity> Orders { get; set; }
        public virtual DbSet<OrderLineEntity> OrderLines { get; set; }
        public virtual DbSet<UserEntity> Users { get; set; }

    }
}
