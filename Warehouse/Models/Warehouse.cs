using System.ComponentModel.DataAnnotations;
namespace warehouseRouter.Warehouse.Models
{
    public class WarehouseDirectory
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public List<WarehouseBranchDirectory> Branches { get; set; } = new();
    }

}