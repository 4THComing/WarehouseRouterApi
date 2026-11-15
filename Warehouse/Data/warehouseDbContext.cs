using Microsoft.EntityFrameworkCore;
using warehouseRouter.Warehouse.Models;

namespace warehouseRouter.Warehouse.Data
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
          : base(options)
        {
        }
        protected override void
        OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WarehouseBranchDirectory>()
            .HasOne<WarehouseDirectory>()
            .WithMany(w => w.Branches)
            .HasForeignKey(b => b.WarehouseId);
        }

        public DbSet<WarehouseDirectory> WarehouseDirectory => Set<WarehouseDirectory>();
        public DbSet<WarehouseBranchDirectory> WarehouseBranchDirectory => Set<WarehouseBranchDirectory>();
    } 
}