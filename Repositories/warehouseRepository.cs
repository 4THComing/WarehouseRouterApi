using Microsoft.EntityFrameworkCore;
using warehouseRouter.Models;
using warehouseRouter.Data;
using warehouseRouter.Interfaces;

namespace warehouseRouter.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly WarehouseDbContext _context;

        public WarehouseRepository(WarehouseDbContext context)
        {
            _context = context;
        }

        public async Task<List<WarehouseBranchDirectory>> GetAllAysnc()
        => await _context.WarehouseBranchDirectory.ToListAsync();

        public async Task<WarehouseDirectory?> GetByIdAsync(Guid Id)
        => await _context.WarehouseDirectory.FindAsync(Id); 
        
        public async Task<WarehouseDirectory?> GetByEmailAsync(string Email)
        => await _context.WarehouseDirectory.FirstOrDefaultAsync(e => e.Email == Email);

        public async Task<WarehouseDirectory?> GetPasswordAsync(string PasswordHash)
        => await _context.WarehouseDirectory.FirstOrDefaultAsync(p => p.PasswordHash == PasswordHash);
    }
}