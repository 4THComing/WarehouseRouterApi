using warehouseRouter.Models;

namespace warehouseRouter.Interfaces
{
    public interface IWarehouseRepository
    {
        Task<List<WarehouseBranchDirectory>> GetAllAysnc();

        Task<WarehouseDirectory?> GetByIdAsync(Guid Id);

        Task<WarehouseDirectory?> GetByEmailAsync(string Email);

        Task<WarehouseDirectory?> GetPasswordAsync(string PasswordHash);

    }
}