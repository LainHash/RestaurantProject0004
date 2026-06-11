using System.Net;

namespace Restaurant.Application.Common.Models
{
    public class Result
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public static Result Success(string? message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result
            {
                IsSuccess = true,
                Message = message,
                StatusCode = (int)statusCode
            };
        }

        public static Result Fail(string? message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Result
            {
                IsSuccess = false,
                Message = message,
                StatusCode = (int)statusCode
            };
        }

    }

    public class Result<T> : Result
    {
        public T? Data { get; set; }

        public static Result<T> Success(T? data, string? message, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            return new Result<T>
            {
                IsSuccess = true,
                Message = message,
                Data = data ?? default,
                StatusCode = (int)statusCode
            };
        }

        public static new Result<T> Fail(string? message, HttpStatusCode statusCode = HttpStatusCode.BadRequest)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Message = message,
                Data = default,
                StatusCode = (int)statusCode
            };
        }
    }
}
