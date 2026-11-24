using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.DTOs
{
    /// <summary>
    /// warehouse fields to be updated
    /// </summary>
    public sealed record UpdateWarehouseDTO
    {
        [Required, EmailAddress, MaxLength(254)]
        public string UpdatedEmail {get; init;} = string.Empty;

        [Required, MinLength(12), MaxLength(128)]
        public string UpdatedPassword { get; init; } = string.Empty;
    }
    /// <summary>
    /// branch fields to be updated
    /// </summary>
    public sealed record UpdateWarehouseBranchDTO
    {
        [Required]
        public string UpdatedLocation { get; init; } = string.Empty;
    }
    /// <summary>
    /// warehouse info returned by Api
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="Email"></param>
    public sealed record WarehouseResponseDTO(Guid Id, string Email);
}
