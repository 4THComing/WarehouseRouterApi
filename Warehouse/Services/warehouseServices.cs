using Microsoft.AspNetCore.Identity;
using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.DTOs;
using warehouseRouter.Warehouse.Interfaces;

namespace warehouseRouter.Warehouse.Services
{
    public class WarehouseService : IwarehouseServices
    {
        private readonly IWarehouseRepository _warehouseRepository;
        private readonly IPasswordHasher<WarehouseDirectory> _passwordHasher;

        public WarehouseService(
            IWarehouseRepository warehouseRepository,
            IPasswordHasher<WarehouseDirectory> passwordHasher)
        {
            _warehouseRepository = warehouseRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<List<WarehouseResponseDTO>> GetAllAsync()
        {
            var warehouses = await _warehouseRepository.GetAllAsync();
            return warehouses.Select(ToResponse).ToList();
        }

        public async Task<WarehouseResponseDTO?> GetByIdAsync(Guid id)
        {
            var warehouse = await _warehouseRepository.GetByIdAsync(id);
            return warehouse is null ? null : ToResponse(warehouse);
        }

        public async Task<WarehouseResponseDTO> AddAsync(CreateWarehouseDTO request)
        {
            ArgumentNullException.ThrowIfNull(request);
            var warehouseInput = request.Warehouse
                ?? throw new ArgumentException("Warehouse details are required.", nameof(request));
            var branchInput = request.Branch
                ?? throw new ArgumentException("Branch details are required.", nameof(request));

            var warehouse = new WarehouseDirectory
            {
                Email = warehouseInput.Email
            };
            warehouse.PasswordHash = _passwordHasher.HashPassword(warehouse, warehouseInput.Password);

            var branch = new WarehouseBranchDirectory
            {
                WarehouseId = warehouse.Id,
                Location = branchInput.Location
            };

            await _warehouseRepository.AddAsync(warehouse, branch);
            return ToResponse(warehouse);
        }

        public async Task<WarehouseResponseDTO?> UpdateWarehouseAsync(Guid id, UpdateWarehouseDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);

            var warehouse = await _warehouseRepository.GetByIdAsync(id);
            if (warehouse is null)
            {
                return null;
            }

            warehouse.Email = dto.UpdatedEmail;
            warehouse.PasswordHash = _passwordHasher.HashPassword(warehouse, dto.UpdatedPassword);
            await _warehouseRepository.UpdateWarehouseAsync(warehouse);
            return ToResponse(warehouse);
        }

        public Task<WarehouseBranchDirectory?> UpdateBranchAsync(Guid id, UpdateWarehouseBranchDTO dto)
        {
            ArgumentNullException.ThrowIfNull(dto);
            return _warehouseRepository.UpdateBranchAsync(id, dto);
        }

        public Task<WarehouseDirectory?> DeleteWarehouseAsync(Guid id)
        {
            return _warehouseRepository.DeleteWarehouseAsync(id);
        }

        public Task<WarehouseBranchDirectory?> DeleteBranchAsync(Guid id)
        {
            return _warehouseRepository.DeleteBranchAsync(id);
        }

        private static WarehouseResponseDTO ToResponse(WarehouseDirectory warehouse)
            => new(warehouse.Id, warehouse.Email);
    }
}