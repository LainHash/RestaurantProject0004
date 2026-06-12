using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.Categories.DTOs;

namespace Restaurant.Application.Features.Catalog.Categories.Commands.Restore
{
    public class RestoreCategoryCommand : IRequest<Result>
    {
        public Guid Id { get; set; }
        public RestoreCategoryCommand(Guid id)
        {
            Id = id;
        }
    }
}
