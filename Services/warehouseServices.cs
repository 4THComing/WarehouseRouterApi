using warehouseRouter.Models;
using System.Text.Json;
using System.Threading.Tasks;
using warehouseRouter.Data;
using Microsoft.EntityFrameworkCore;
using warehouseRouter.DTOs;

namespace warehouseRouter.Services
{
    public class WarehouseService
    {
        private readonly WarehouseDbContext _context;
        private readonly string filePath = "Data/warehouseData.Json";

        public Dictionary<WarehouseDirectory, List<WarehouseBranchDirectory>> warehouses = new();

        public WarehouseService(WarehouseDbContext context)
        {
            _context = context;
            Directory.CreateDirectory(Path.GetDirectoryName(filePath)!);
        }

        private readonly HashSet<Guid> warehouseIds = new();
        private readonly HashSet<Guid> warehouseBranchIds = new();

        public async Task LoadWarehousesAysnc()
        {
           warehouseIds.Clear();
           warehouseBranchIds.Clear();

           var warehouseRecords = await _context.WarehouseDirectory.ToListAsync();
           var branchRecords = await _context.WarehouseBranchDirectory.ToListAsync();

           foreach(var warehouse in warehouseRecords)
            {
                warehouseIds.Add(warehouse.Id);
            }

            foreach(var branch in branchRecords)
            {
                warehouseBranchIds.Add(branch.Id);
            }

        }
         
        public async Task<List<WarehouseBranchDirectory>> GetAllAysnc()
        => await _context.WarehouseBranchDirectory.ToListAsync();

        public async Task<WarehouseDirectory?> GetByIdAsync(Guid Id)
        => await _context.WarehouseDirectory.FindAsync(Id); 

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

            _context.WarehouseDirectory.Add(warehouse);
            _context.WarehouseBranchDirectory.Add(branch);
            await _context.SaveChangesAsync();

            // add IDs if they don't exist
            warehouseIds.Add(warehouse.Id);
            warehouseBranchIds.Add(branch.Id);
        }

       public async Task<WarehouseDirectory> UpdateWarehouseAsync(Guid Id, UpdateWarehouseDTO dto)
        {
            await LoadWarehousesAysnc();

            //retrieve warehouse
            var toUpdate = await _context.WarehouseDirectory.FindAsync(Id);

            //check if warehouse exists
            if (toUpdate == null)
            throw new InvalidOperationException ("No warehouse to update");

            //update warehouse Details 
            toUpdate.Email = dto.UpdatedEmail;
            toUpdate.PasswordHash = dto.UpdatedPasswordHash;
            
            //Save update
            await _context.SaveChangesAsync();

            return toUpdate;
        } 

        public async Task<WarehouseBranchDirectory> UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO dto)
        {
            await LoadWarehousesAysnc();

            // retrieve branch
            var branchToUpdate = await _context.WarehouseBranchDirectory.FindAsync(Id);

            // check if branch exists
            if (branchToUpdate == null)
            throw new InvalidOperationException("No Branch to update");

            //update branch details
            branchToUpdate.Location = dto.UpdatedLocation;

            // save update
            await _context.SaveChangesAsync();

            return branchToUpdate;
        }

       public async Task DeleteWarehouseAsync(Guid Id)
        {
            await LoadWarehousesAysnc();

            // retrieve warehouse
            var toDelete = await _context.WarehouseDirectory.FindAsync(Id);
            
            // Check if warehouse exists
            if (toDelete == null)
            throw new InvalidOperationException ("No warehouse to delete");
            
            // retrive warehouse branched linked to the warehouse
            var toDeleteBranch = await _context.WarehouseBranchDirectory.Where(branch => branch.WarehouseId == Id).ToListAsync();
            
            //remove warehouse and branches
            _context.WarehouseDirectory.Remove(toDelete);
            _context.WarehouseBranchDirectory.RemoveRange(toDeleteBranch);

            //save deletion 
            await _context.SaveChangesAsync();
            
            // remove warehouse and branches from hashsets
            warehouseIds.Remove(toDelete.Id);

            foreach(var branch in toDeleteBranch)
            {
              warehouseBranchIds.Remove(branch.Id);
            }
        } 

        public async Task DeleteBranchAsync(Guid Id)
        {
            await LoadWarehousesAysnc();

            // retrieve branch
            var branchToDelete = await _context.WarehouseBranchDirectory.FindAsync(Id);

            // check if branch exists
            if (branchToDelete == null)
            throw new InvalidOperationException("No Branch to delete");

            // remove branch
            _context.WarehouseBranchDirectory.Remove(branchToDelete);

            // save deletion
            await _context.SaveChangesAsync();

            // remove branch from hashset
            warehouseBranchIds.Remove(Id);
        } 

    }
}