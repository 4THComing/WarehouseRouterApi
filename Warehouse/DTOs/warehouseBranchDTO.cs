using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.DTOs

{
    /// <summary>
    /// branch details supplied when creating a branch
    /// </summary>
    public sealed record WarehouseBranchDTO
    {
        [Required]
        public string Location { get; init; } = string.Empty;
    }
}

