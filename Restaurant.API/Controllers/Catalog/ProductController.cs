using MediatR;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Application.Features.Catalog.Products.Queries.GetByPublicId;
using Restaurant.Application.Features.Catalog.Products.Queries.GetAll;
using Restaurant.Application.Features.Catalog.Products.DTOs;
using Restaurant.Application.Features.Catalog.Products.Commands.Create;
using Restaurant.Application.Features.Catalog.Products.Commands.Update;
using Restaurant.Application.Features.Catalog.Products.Commands.ChangeImages;
using Restaurant.Application.Features.Catalog.Products.Commands.Delete;
using Restaurant.Application.Features.Catalog.Products.Commands.Restore;
using Restaurant.Application.Common.Models;

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
        public async Task<IActionResult> GetAll([FromQuery] ProductQuery query)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(query));
            return StatusCode(result.StatusCode, result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOne([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new GetOneProductQuery(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateProductDTO createProductDTO)
        {
            var result = await _mediator.Send(new CreateProductCommand(createProductDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateProductDTO updateProductDTO)
        {
            var result = await _mediator.Send(new UpdateProductCommand(id, updateProductDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id}/images")]
        public async Task<IActionResult> ChangeImages([FromRoute] Guid id, [FromBody] ChangeImagesProductDTO changeImagesProductDTO)
        {
            var result = await _mediator.Send(new ChangeImagesProductCommand(id, changeImagesProductDTO));
            return StatusCode(result.StatusCode, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id));
            return StatusCode(result.StatusCode, result);
        }

        [HttpPatch("{id}/restore")]
        public async Task<IActionResult> Restore([FromRoute] Guid id)
        {
            var result = await _mediator.Send(new RestoreProductCommand(id));
            return StatusCode(result.StatusCode, result);
        }
    }
}
