using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Products.DTOs;

namespace Restaurant.Application.Features.Catalog.Products.Commands.ChangeImages
{
    public class ChangeImagesProductCommand : IRequest<Result<ProductDTO>>
    {
        public Guid Id { get; set; }
        public ChangeImagesProductDTO ChangeImagesProductDTO { get; set; } = null!;

        public ChangeImagesProductCommand(Guid id, ChangeImagesProductDTO changeImagesProductDTO)
        {
            Id = id;
            ChangeImagesProductDTO = changeImagesProductDTO;
        }
    }
}
