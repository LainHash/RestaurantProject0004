using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Products.Queries.GetByPublicId;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;

namespace Restaurant.API.Controllers.Catalog
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _mediator.Send(new GetAllProductsQuery());
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetOneProductQuery(id));
            return StatusCode(result.StatusCode, result);
        }
    }
}
