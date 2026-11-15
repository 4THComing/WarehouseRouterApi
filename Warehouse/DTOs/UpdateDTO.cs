using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.DTOs
{
    public class UpdateWarehouseDTO
    {
        [Required, EmailAddress, MaxLength(254)]
        public string UpdatedEmail {get; set;} = string.Empty;

        [Required, MinLength(12), MaxLength(128)]
        public string UpdatedPassword { get; set; } = string.Empty;
    }

    public class UpdateWarehouseBranchDTO
    {
        [Required]
        public string UpdatedLocation { get; set; } = string.Empty;
    }

    public sealed record WarehouseResponseDTO(Guid Id, string Email);
}
