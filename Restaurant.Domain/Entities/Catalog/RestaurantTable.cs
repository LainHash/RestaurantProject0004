using Restaurant.Domain.Common.Models;

namespace Restaurant.Domain.Entities.Catalog
{
    public class RestaurantTable : SoftDeleteEntity
    {
        public int TableNumber { get; set; }
        public int FloorNumber { get; set; }
        public int Capacity { get; set; }
        public string Shape { get; set; } = null!;
        public string Status { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;

        public RestaurantTable() { }

        public RestaurantTable(int tableNumber, int floorNumber, int capacity, string shape, string status, string? description = "")
        {
            TableNumber = tableNumber;
            FloorNumber = floorNumber;
            Capacity = capacity;
            Shape = shape;
            Status = status;
            Description = description;
        }

        public void Update(int tableNumber, int floorNumber, int capacity, string shape, string status, string? description = "")
        {
            TableNumber = tableNumber;
            FloorNumber = floorNumber;
            Capacity = capacity;
            Shape = shape;
            Status = status;
            Description = description;
            UpdatedAt = DateTime.UtcNow;
        }

        public void SoftDelete()
        {
            IsDeleted = true;
            UpdatedAt = DateTime.UtcNow;
            DeletedAt = DateTime.UtcNow;
        }

        public void Restore()
        {
            IsDeleted = false;
            UpdatedAt = DateTime.UtcNow;
            DeletedAt = null;
        }
    }
}

