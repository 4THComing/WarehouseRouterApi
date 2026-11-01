using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.DTOs
{
    public class WarehouseDTO
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;
    }
}