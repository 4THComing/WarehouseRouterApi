using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.DTOs
{
    /// <summary>
    /// payload for creating a warehouse and its initial branch
    /// </summary>
    public sealed record CreateWarehouseDTO
    {
        [Required]
        public WarehouseDTO? Warehouse { get; init; }

        [Required]
        public WarehouseBranchDTO? Branch { get; init; }
    }
}