using MediatR;
using Restaurant.Application.Common.Models;
using Restaurant.Application.Features.Catalog.RestaurantTables.DTOs;
using Restaurant.Application.Interfaces.Repositories.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.Queries.GetAllByFloor
{
    internal class GetAllTablesByFloorHandler : IRequestHandler<GetAllTablesByFloorQuery, Result<List<RestaurantTableDTO>>>
    {
        private readonly IRestaurantTableRepository _restaurantTableRepository;
        public GetAllTablesByFloorHandler(IRestaurantTableRepository restaurantTableRepository)
        {
            _restaurantTableRepository = restaurantTableRepository;
        }

        public async Task<Result<List<RestaurantTableDTO>>> Handle(GetAllTablesByFloorQuery request, CancellationToken cancellationToken)
        {
            var response = await _restaurantTableRepository
                .GetAllByFloorAsync(request.Floor, cancellationToken);
            return response;
        }
    }
}
