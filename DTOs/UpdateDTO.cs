using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.DTOs
{
    public class UpdateWarehouseDTO
    {
        [Required]
        public string UpdatedEmail {get; set;} = string.Empty;

        [Required]
        public string UpdatedPasswordHash { get; set; } = string.Empty;
    }

    public class UpdateWarehouseBranchDTO
    {
        [Required]
        public string UpdatedLocation { get; set; } = string.Empty;
    }

}
