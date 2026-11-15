using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.DTOs
{
    public class WarehouseDTO
    {
        [Required, EmailAddress, MaxLength(254)]
        public string Email { get; set; } = string.Empty;

        [Required, MinLength(12), MaxLength(128)]
        public string Password { get; set; } = string.Empty;
    }
}