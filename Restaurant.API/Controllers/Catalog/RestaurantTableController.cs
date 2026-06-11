using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Create;
using Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Delete;
using Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Restore;
using Restaurant.Application.Features.Catalog.RestaurantTables.Commands.Update;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetAll;
using Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetAllByFloor;
using Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetOneByNumber;

namespace Restaurant.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantTableController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RestaurantTableController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllTablesQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{floor}")]
        public async Task<IActionResult> Get([FromRoute] int floor)
        {
            var result = await _mediator.Send(new GetAllTablesByFloorQuery(floor));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{floor}/{number}")]
        public async Task<IActionResult> GetOneByNumber(int floor, int number)
        {
            var result = await _mediator.Send(new GetOneTableByNumberQuery(floor, number));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateRestaurantTableDTO createRestaurantTableDTO)
        {
            var result = await _mediator.Send(new CreateRestaurantTableCommand(createRestaurantTableDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateRestaurantTableDTO updateRestaurantTableDTO)
        {
            var result = await _mediator.Send(new UpdateRestaurantTableCommand(id, updateRestaurantTableDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteRestaurantTableCommand(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new RestoreRestaurantTableCommand(id));
            return StatusCode(result.StatusCode, result);
        }
    }
}
