using Restaurant.Application.Common.Enums;

namespace Restaurant.Application.Common.Models
{
    public class ProductQuery
    {
        public string? Keyword { get; set; }

        public string? CategoryName { get; set; }

        public string? SortBy { get; set; } = nameof(SortType.CreateAtDesc);

        public int Page { get; set; } = 1;

        public int PageSize { get; set; } = 10;
    }
}
