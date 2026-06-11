using Restaurant.Application.Common.Models;
using Restaurant.Domain.Entities.Catalog;

namespace Restaurant.Application.Features.Catalog.RestaurantTables.DTOs
{
    public class RestaurantTableDTO : SoftDeleteDTO
    {
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public int Capacity { get; set; }
        public string Shape { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;

        public RestaurantTableDTO(RestaurantTable table)
        {
            PublicId = table.PublicId;
            TableNumber = table.TableNumber;
            FloorNumber = table.FloorNumber;
            Capacity = table.Capacity;
            Shape = table.Shape;
            Status = table.Status;
            Description = table.Description ?? string.Empty;
            CreatedAt = table.CreatedAt;
            UpdatedAt = table.UpdatedAt;
            IsDeleted = table.IsDeleted;
            DeletedAt = table.DeletedAt;
        }
    }
}
