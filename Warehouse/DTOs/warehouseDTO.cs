using System.ComponentModel.DataAnnotations;

namespace warehouseRouter.Warehouse.DTOs
{
    /// <summary>
    /// warehouse details supplied when creating or updating a warehouse
    /// </summary>
    public sealed record WarehouseDTO // sealed record makes the Data type immutable by default
    {
        [Required, EmailAddress, MaxLength(254)]  
        public string Email { get; init; } = string.Empty; // get; init; allows a value to be set during initialisation

        [Required, MinLength(12), MaxLength(128)]
        public string Password { get; init; } = string.Empty;
    }

}