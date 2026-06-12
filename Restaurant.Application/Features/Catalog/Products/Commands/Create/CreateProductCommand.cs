using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Features.Catalog.Products.Commands.Create
{
    public class CreateProductCommand : IRequest<Result<ProductDTO>>
    {
        public CreateProductDTO CreateProductDTO { get; set; } = null!;
        public CreateProductCommand(CreateProductDTO createProductDTO)
        {
            CreateProductDTO = createProductDTO;
        }
    }
}
