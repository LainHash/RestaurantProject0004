using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Delete
{
    public class DeleteCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public DeleteCategoryCommand(Guid id)
        {
            Id = id;
        }
    }
}
