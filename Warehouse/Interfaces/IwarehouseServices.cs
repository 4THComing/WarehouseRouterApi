using warehouseRouter.Warehouse.DTOs;

namespace warehouseRouter.Warehouse.Interfaces
{
    public interface IWarehouseServices
    {
        Task<List<WarehouseResponseDTO>> GetAllAsync();
        Task<WarehouseResponseDTO?> GetByIdAsync(Guid Id);
        Task<WarehouseResponseDTO> AddAsync(CreateWarehouseDTO request);

        Task<WarehouseResponseDTO?> UpdateWarehouseAsync(Guid Id, UpdateWarehouseDTO dto);

        Task<WarehouseBranchResponseDTO?> UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO dto);

        Task<bool> DeleteWarehouseAsync(Guid Id);

        Task<bool> DeleteBranchAsync(Guid Id);
    }
}