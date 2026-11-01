using System.ComponentModel.DataAnnotations;
namespace warehouseRouter.Models
{
    public class WarehouseDirectory
    {
        [Key]
        public Guid Id { get; set;}= Guid.NewGuid();
        
        [Required]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; }= string.Empty;

        public List<WarehouseBranchDirectory> Branches { get; set;} = new();
    }
}