using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.DTOs;

namespace warehouseRouter.Warehouse.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<List<WarehouseDirectory>> GetAllAsync();

        Task<List<WarehouseBranchDirectory>> GetAllBranchesAsync();

        Task<WarehouseDirectory?> GetByIdAsync(Guid Id);

        Task<WarehouseBranchDirectory?> GetBranchByIdAsync(Guid Id);

        Task<WarehouseDirectory?> GetByEmailAsync(string Email);

        Task AddAsync(WarehouseDirectory warehouse, WarehouseBranchDirectory branch);

        Task<WarehouseDirectory?> UpdateWarehouseAsync(Guid id, UpdateWarehouseDTO dto);

        Task UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO dto);

        Task DeleteWarehouseAsync(Guid Id);

        Task<List<Guid>> GetBranchIdsByWarehouseIdAsync(Guid Id);

        Task DeleteBranchAsync(Guid Id);

    }
}