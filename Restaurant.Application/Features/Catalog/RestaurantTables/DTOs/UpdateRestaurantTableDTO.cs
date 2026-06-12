namespace Restaurant.Application.Features.Catalog.RestaurantTables.DTOs
{
    public class UpdateRestaurantTableDTO
    {
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public int Capacity { get; set; }
        public string Shape { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
    }
}
