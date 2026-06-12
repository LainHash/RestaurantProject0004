namespace Restaurant.Blazor.Common.Models
{
    public class PageApiResponse<T> : ApiResponse<T>
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}
