using Microsoft.EntityFrameworkCore;
using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.Data;
using warehouseRouter.Warehouse.Interfaces;
using warehouseRouter.Warehouse.DTOs;

namespace warehouseRouter.Warehouse.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly WarehouseDbContext _context;

        public WarehouseRepository(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<WarehouseDirectory>> GetAllAsync()
        => await _context.WarehouseDirectory.ToListAsync();

        public async Task<WarehouseDirectory?> GetByIdAsync(Guid Id)
        => await _context.WarehouseDirectory.FindAsync(Id);

        public async Task<WarehouseBranchDirectory?> GetBranchByIdAsync(Guid Id)
        => await _context.WarehouseBranchDirectory.FindAsync(Id);

        public async Task<WarehouseDirectory?> GetByEmailAsync(string Email)
        => await _context.WarehouseDirectory.FirstOrDefaultAsync(e => e.Email == Email);

        public async Task AddAsync(WarehouseDirectory warehouse, WarehouseBranchDirectory branch)
        {
            _context.WarehouseDirectory.Add(warehouse);
            _context.WarehouseBranchDirectory.Add(branch);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateWarehouseAsync(WarehouseDirectory warehouse)
        {
            _context.WarehouseDirectory.Update(warehouse);
            await _context.SaveChangesAsync();
        }

        public async Task<WarehouseBranchDirectory?> UpdateBranchAsync(Guid id, UpdateWarehouseBranchDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var branch = await _context.WarehouseBranchDirectory.FindAsync(id);
            if (branch == null)
            {
                return null;
            }

            branch.Location = dto.UpdatedLocation;
            await _context.SaveChangesAsync();
            return branch;
        }

        public async Task<WarehouseDirectory?> DeleteWarehouseAsync(Guid Id)
        {
            var warehouse = await _context.WarehouseDirectory.FindAsync(Id);

            if (warehouse == null)
            {
                return null;
            }

            var branches = await _context.WarehouseBranchDirectory.Where(branch => branch.WarehouseId == Id).ToListAsync();

            _context.WarehouseBranchDirectory.RemoveRange(branches);
            _context.WarehouseDirectory.Remove(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<WarehouseBranchDirectory?> DeleteBranchAsync(Guid Id)
        {
           var branch = await _context.WarehouseBranchDirectory.FindAsync(Id);
           if (branch == null)
            {
                return null;
            }

            _context.WarehouseBranchDirectory.Remove(branch);
            await _context.SaveChangesAsync();
            return branch;
        }

    }
}