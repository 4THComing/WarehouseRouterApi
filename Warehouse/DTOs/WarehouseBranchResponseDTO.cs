namespace warehouseRouter.Warehouse.DTOs
{
    /// <summary>
    /// Branch details returned by API
    /// </summary>
    /// <param name="Id"></param>
    /// <param name="WarehouseId"></param>
    /// <param name="Location"></param>
    public sealed record WarehouseBranchResponseDTO(
       Guid Id,
       Guid WarehouseId,
       string Location
       );

}