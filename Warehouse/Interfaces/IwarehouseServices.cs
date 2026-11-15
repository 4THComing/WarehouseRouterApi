using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.DTOs;


namespace warehouseRouter.Warehouse.Interfaces
{
    public interface IwarehouseServices
    {
        Task<List<WarehouseDirectory>> GetAllAsync();
        Task<List<WarehouseDirectory?>> GetByIdAsync(Guid Id);
        Task AddAsync(WarehouseDirectory warehouse, WarehouseBranchDirectory branch);

        Task<WarehouseDirectory?> UpdateWarehouseAsync(Guid Id, UpdateWarehouseDTO dto);

        Task<WarehouseBranchDirectory?> UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO dto);

        Task<WarehouseDirectory?> DeleteWarehouseAsync(Guid Id);

        Task<WarehouseBranchDirectory?> DeleteBranchAsync(Guid Id);
    }
}