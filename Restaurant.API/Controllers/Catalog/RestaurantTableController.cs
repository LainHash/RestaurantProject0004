using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
