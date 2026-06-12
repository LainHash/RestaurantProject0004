using System.Net;

namespace Restaurant.Application.Common.Models
{
    public class PageResult<T> : Result<T> where T : class
    {
        public int TotalItems { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }

        public static PageResult<T> Success(T? data, string? message, int totalItems, int page, int pageSize, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new PageResult<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data ?? default,
                TotalItems = totalItems,
                Page = page,
                PageSize = pageSize,
                StatusCode = (int)statusCode
            };
        }

        public static new PageResult<T> Fail(string? message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new PageResult<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default,
                StatusCode = (int)statusCode
            };
        }
    }
}
