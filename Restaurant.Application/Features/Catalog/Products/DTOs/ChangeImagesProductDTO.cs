namespace Restaurant.Application.Features.Catalog.Products.DTOs
{
    public class ChangeImagesProductDTO
    {
        public List<string> ImagesToAdd { get; set; } = new List<string>();
        public List<string> ImagesToRemove { get; set; } = new List<string>();

        public string? NewPrimaryImageUrl { get; set; }
    }
}
