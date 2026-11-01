using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.DTOs

{
    public class WarehouseBranchDTO
    {

        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        public string Location { get; set; } = string.Empty;
    }
}

