using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.DTOs

{
    public class WarehouseBranchDTO
    {
        [Required]
        public string Location { get; set; } = string.Empty;
    }
}

