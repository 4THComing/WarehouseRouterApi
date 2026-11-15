using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.DTOs
{
    public class CreateWarehouseDTO
    {
        [Required]
        public WarehouseDTO? Warehouse { get; set; }

        [Required]
        public WarehouseBranchDTO? Branch { get; set; } 
    }
}