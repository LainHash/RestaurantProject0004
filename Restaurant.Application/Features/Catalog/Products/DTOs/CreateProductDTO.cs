namespace Restaurant.Application.Features.Catalog.Products.DTOs
{
    public class CreateProductDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; } = string.Empty;
        public bool IsMadeToOrder { get; set; }

        public decimal Price { get; set; }
        public string Unit { get; set; } = null!;
        public decimal Quantity { get; set; }

        public string CategoryName { get; set; } = null!;

        public List<string> Images { get; set; } = new List<string>();
    }
}

