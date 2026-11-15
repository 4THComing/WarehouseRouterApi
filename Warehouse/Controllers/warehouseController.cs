using Microsoft.AspNetCore.Mvc;
using warehouseRouter.Warehouse.Interfaces;
using warehouseRouter.Warehouse.Repositories;
using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.Services;
using warehouseRouter.Warehouse.DTOs;
using warehouseRouter.Warehouse.Data;

namespace warehouseRouter.Warehouse.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class WarehouseController : ControllerBase
    {
        private readonly IwarehouseServices _warehouseService;

        public WarehouseController(IwarehouseServices warehouseService)
        {
            _warehouseService = warehouseService;
        }

        [HttpGet]
        public async Task<ActionResult<List<WarehouseDirectory>>> GetAllAsync()
        => await _warehouseService.GetAllAsync();

        [HttpGet("{Id}")]
        public async Task<ActionResult<WarehouseDirectory>> GetByIdAsync(Guid Id)
        {
            var warehouse = await _warehouseService.GetByIdAsync(Id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

        [HttpPost]
        public async Task<ActionResult<WarehouseDirectory>> AddWarehouse([FromBody] CreateWarehouseDTO request)
        {
            if (request.Warehouse == null)
            {
                return BadRequest("Provide Warehouse Details");
            }

            if (request.Branch == null)
            {
                return BadRequest("Provide Warehouse Details");
            }

            var newWarehouse = new WarehouseDirectory
            {
                Email = request.Warehouse.Email,
                PasswordHash = request.Warehouse.PasswordHash
            };

            var newWarehouseBranch = new WarehouseBranchDirectory
            {
                Location = request.Branch.Location
            };

            await _warehouseService.AddAsync(newWarehouse, newWarehouseBranch);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { Id = newWarehouse.Id },
                newWarehouse);
        }

        [HttpPut("warehouse/{Id}")]
        public async Task<ActionResult<WarehouseDirectory>> UpdateWarehouseAsync(Guid Id, UpdateWarehouseDTO dto)
        {
            var toUpdateWarehouse = await _warehouseService.UpdateWarehouseAsync(Id, dto);
            if (toUpdateWarehouse == null)
                return NotFound();

            return Ok(toUpdateWarehouse);

        }

        [HttpPut("branches/{Id}")]
        public async Task<ActionResult<WarehouseBranchDirectory>> UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO branchUpdate)
        {
            if (branchUpdate == null)
                return BadRequest();

            await _warehouseService.UpdateBranchAsync(Id, branchUpdate);

            return Ok(branchUpdate);
        }

        [HttpDelete("branches/{Id:guid}")]
        public async Task<ActionResult<WarehouseBranchDirectory>> DeleteBranchAsync(Guid Id)
        {
            await _warehouseService.DeleteBranchAsync(Id);

            return NoContent();
        }

        [HttpDelete("warehouse/{Id:guid}")]
        public async Task<ActionResult<WarehouseDirectory>> DeleteWarehouseAsync(Guid Id)
        {
            await _warehouseService.DeleteWarehouseAsync(Id);

            return NoContent();
        }

    }
}