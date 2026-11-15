using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.DTOs;
using warehouseRouter.Warehouse.Interfaces;

namespace warehouseRouter.Warehouse.Services
{
    public class WarehouseService : IwarehouseServices
    {

        private readonly IWarehouseRepository _warehouseRepository;

        public WarehouseService(IWarehouseRepository warehouseRepository )
        {
            _warehouseRepository = warehouseRepository;
        }

        public Dictionary<WarehouseDirectory, List<WarehouseBranchDirectory>> warehouses = new();

        private readonly HashSet<Guid> warehouseIds = new();
        private readonly HashSet<Guid> warehouseBranchIds = new();

        public async Task<List<WarehouseDirectory>> GetAllAsync()
        {
            return await _warehouseRepository.GetAllAsync();
        }

        public async Task<WarehouseDirectory?> GetByIdAsync(Guid Id)
        {
            return await _warehouseRepository.GetByIdAsync(Id);
        }

        public async Task LoadWarehousesAysnc()
        {
           warehouseIds.Clear();
           warehouseBranchIds.Clear();

           var warehouseRecords = await _warehouseRepository.GetAllAsync();
           var branchRecords = await _warehouseRepository.GetAllBranchesAsync();

           foreach(var warehouse in warehouseRecords)
            {
                warehouseIds.Add(warehouse.Id);
            }

            foreach(var branch in branchRecords)
            {
                warehouseBranchIds.Add(branch.Id);
            }

        }

        public async Task AddAsync(WarehouseDirectory warehouse, WarehouseBranchDirectory branch)
        {
            await LoadWarehousesAysnc();
            // check if the warehouse ID already exists
            if (warehouseIds.Contains(warehouse.Id) || warehouseBranchIds.Contains(warehouse.Id))
            {
                throw new ArgumentException("Duplicate Id found", nameof(warehouseBranchIds));
            }

            // check if the branch ID already exists
            if (warehouseIds.Contains(branch.Id) || warehouseBranchIds.Contains(branch.Id))
            {
                throw new ArgumentException("Duplicate Id found", nameof(warehouseBranchIds));
            }

            branch.WarehouseId = warehouse.Id;

            // add IDs if they don't exist
            warehouseIds.Add(warehouse.Id);
            warehouseBranchIds.Add(branch.Id);

            // add through repository
            await _warehouseRepository.AddAsync(warehouse, branch);
        }

       public Task<WarehouseDirectory?> UpdateWarehouseAsync(Guid id, UpdateWarehouseDTO dto)
        {
          ArgumentNullException.ThrowIfNull(dto);
          return _warehouseRepository.UpdateWarehouseAsync(id, dto);
        } 

        public async Task<WarehouseBranchDirectory?> UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO dto)
        {
            await LoadWarehousesAysnc();

            // retrieve branch
            var branchToUpdate = await _warehouseRepository.GetBranchByIdAsync(Id);

            // check if branch exists
            if (branchToUpdate == null)
            throw new InvalidOperationException("No Branch to update");

            //update branch details
            branchToUpdate.Location = dto.UpdatedLocation;

            // save update
            await _warehouseRepository.UpdateBranchAsync(Id, dto);

            return branchToUpdate;
        }

       public async Task<WarehouseDirectory?> DeleteWarehouseAsync(Guid Id)
        {
            await LoadWarehousesAysnc();

            if (!warehouseIds.Contains(Id))
            {
                throw new InvalidOperationException("No Warehouse to delete");
            }
            
            // get branchIds before deletion(calling the method)
            var branchIds = await _warehouseRepository.GetBranchIdsByWarehouseIdAsync(Id);

            // delete through repository
            await _warehouseRepository.DeleteWarehouseAsync(Id);
            
            warehouseIds.Remove(Id);

            foreach (var branchId in branchIds )
            {
                warehouseBranchIds.Remove(branchId);
            }
        } 

        public async Task DeleteBranchAsync(Guid Id)
        {
            await LoadWarehousesAysnc();

            // retrieve branch
            var toDeleteBranch = await _warehouseRepository.GetBranchIdsByWarehouseIdAsync(Id);

            // check if branch exists
            if (toDeleteBranch == null)
            throw new InvalidOperationException("No Branch to delete");

            // remove branch
            await _warehouseRepository.DeleteBranchAsync(Id);

            // remove branch from hashset
            warehouseBranchIds.Remove(Id);
        } 

    }
}