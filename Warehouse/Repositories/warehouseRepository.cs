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

        public async Task<List<WarehouseBranchDirectory>> GetAllBranchesAsync()
        => await _context.WarehouseBranchDirectory.ToListAsync();

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

        public async Task<WarehouseDirectory?> UpdateWarehouseAsync(Guid id, UpdateWarehouseDTO dto)
        {
           ArgumentNullException.ThrowIfNull(dto);

           var warehouse = await _context.WarehouseDirectory.FindAsync(id);
           if(warehouse == null)
           {
                return null;
           }

            warehouse.Email = dto.UpdatedEmail;
            warehouse.PasswordHash = dto.UpdatedPasswordHash;
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO dto)
        {
            
        }

        public async Task DeleteWarehouseAsync(Guid Id)
        {
            var toDelete = await _context.WarehouseDirectory.FindAsync(Id);
            var toDeleteBranch = await _context.WarehouseBranchDirectory.Where(branch => branch.WarehouseId == Id).ToListAsync();

            if (toDelete == null)
                throw new InvalidOperationException("No warehouse to delete");
            //remove warehouse and branches
            _context.WarehouseDirectory.Remove(toDelete);
            _context.WarehouseBranchDirectory.RemoveRange(toDeleteBranch);

            //save deletion 
            await _context.SaveChangesAsync();
        }

        public async Task<List<Guid>> GetBranchIdsByWarehouseIdAsync(Guid Id)
        {
            return await _context.WarehouseBranchDirectory.Where(branch => branch.WarehouseId == Id).Select(branch => branch.Id).ToListAsync();
        }

        public async Task DeleteBranchAsync(Guid Id)
        {
            var toDeleteBranch = await _context.WarehouseBranchDirectory.Where(branch => branch.WarehouseId == Id).ToListAsync();

            if (toDeleteBranch == null)
                throw new InvalidOperationException("No Branch to delete");

            _context.WarehouseBranchDirectory.RemoveRange(toDeleteBranch);

            await _context.SaveChangesAsync();    
        }

    }
}