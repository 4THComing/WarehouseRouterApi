using Microsoft.AspNetCore.Mvc;
using warehouseRouter.Warehouse.Interfaces;
using warehouseRouter.Warehouse.Models;
using warehouseRouter.Warehouse.DTOs;

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
        public async Task<ActionResult<List<WarehouseResponseDTO>>> GetAllAsync()
        => await _warehouseService.GetAllAsync();

        [HttpGet("{Id}")]
        public async Task<ActionResult<WarehouseResponseDTO>> GetByIdAsync(Guid Id)
        {
            var warehouse = await _warehouseService.GetByIdAsync(Id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return Ok(warehouse);
        }

        [HttpPost]
        public async Task<ActionResult<WarehouseResponseDTO>> AddWarehouse([FromBody] CreateWarehouseDTO request)
        {
            if (request?.Warehouse == null)
            {
                return BadRequest("Provide Warehouse Details");
            }

            if (request.Branch == null)
            {
                return BadRequest("Provide Branch Details");
            }

            var warehouse = await _warehouseService.AddAsync(request);

            return CreatedAtAction(
                nameof(GetByIdAsync),
                new { Id = warehouse.Id },
                warehouse);
        }

        [HttpPut("warehouse/{Id}")]
        public async Task<ActionResult<WarehouseResponseDTO>> UpdateWarehouseAsync(Guid Id, UpdateWarehouseDTO dto)
        {
            var warehouse = await _warehouseService.UpdateWarehouseAsync(Id, dto);
            if (warehouse == null)
                return NotFound();

            return Ok(warehouse);

        }

        [HttpPut("branches/{Id}")]
        public async Task<ActionResult<WarehouseBranchDirectory>> UpdateBranchAsync(Guid Id, UpdateWarehouseBranchDTO branchUpdate)
        {
            if (branchUpdate == null)
                return BadRequest();

            var updatedBranch = await _warehouseService.UpdateBranchAsync(Id, branchUpdate);
            if (updatedBranch == null)
                return NotFound();

            return Ok(updatedBranch);
        }

        [HttpDelete("warehouse/{Id:guid}")]
        public async Task<ActionResult<WarehouseDirectory>> DeleteWarehouseAsync(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return BadRequest("Invalid Id");
            }
            var warehouse = await _warehouseService.DeleteWarehouseAsync(Id);
            if (warehouse == null)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("branches/{Id:guid}")]
        public async Task<ActionResult<WarehouseBranchDirectory>> DeleteBranchAsync(Guid Id)
        {
            if (Id == Guid.Empty)
            {
                return BadRequest("Invalid Id");
            }
            var branch = await _warehouseService.DeleteBranchAsync(Id);
            if (branch == null)
            {
                return NotFound();
            }

            return NoContent();
        }


    }
}