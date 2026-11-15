using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.DTOs;

namespace warehouseRouter.Warehouse.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<List<WarehouseDirectory>> GetAllAsync();

        Task<WarehouseDirectory?> GetByIdAsync(Guid Id);

        Task<WarehouseBranchDirectory?> GetBranchByIdAsync(Guid Id);

        Task<WarehouseDirectory?> GetByEmailAsync(string Email);

        Task AddAsync(WarehouseDirectory warehouse, WarehouseBranchDirectory branch);

        Task UpdateWarehouseAsync(WarehouseDirectory warehouse);

        Task<WarehouseBranchDirectory?> UpdateBranchAsync(Guid id, UpdateWarehouseBranchDTO dto);

        Task<WarehouseDirectory?> DeleteWarehouseAsync(Guid Id);

        Task<WarehouseBranchDirectory?> DeleteBranchAsync(Guid Id);

    }
}