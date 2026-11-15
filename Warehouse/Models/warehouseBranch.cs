using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.Models
{
    public class WarehouseBranchDirectory
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public Guid WarehouseId { get; set; }
        
        [Required]
        public string Location { get; set; } = string.Empty;

    }
}