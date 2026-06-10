using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Categories.Queries.GetAll;

namespace Restaurant.API.Controllers.Catalog
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CategoryController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllQuery());
            return StatusCode(result.StatusCode, result);
        }
    }
}
