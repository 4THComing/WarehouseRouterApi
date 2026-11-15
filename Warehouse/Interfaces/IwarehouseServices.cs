using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.DTOs;


namespace warehouseRouter.Warehouse.Interfaces
{
    public interface IwarehouseServices
    {
        Task<List<WarehouseResponseDTO>> GetAllAsync();
        Task<WarehouseResponseDTO?> GetByIdAsync(Guid Id);
        Task<WarehouseResponseDTO> AddAsync(CreateWarehouseDTO request);

        Task<WarehouseResponseDTO?> UpdateWarehouseAsync(Guid Id, UpdateWarehouseDTO dto);

        Task<WarehouseBranchDirectory?> UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO dto);

        Task<WarehouseDirectory?> DeleteWarehouseAsync(Guid Id);

        Task<WarehouseBranchDirectory?> DeleteBranchAsync(Guid Id);
    }
}