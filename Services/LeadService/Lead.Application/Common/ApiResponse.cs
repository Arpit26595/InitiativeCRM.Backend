using System.Collections.Generic;

namespace Lead.Application.Common
{
    /// <summary>
    /// Generic API response wrapper for single item endpoints
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>
    public class ApiResponse<T>
    {
        public T? Data { get; set; }
        public bool Success { get; set; }
        public string? Message { get; set; }
        public Dictionary<string, object>? Metadata { get; set; }

        public static ApiResponse<T> SuccessResponse(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                Data = data,
                Success = true,
                Message = message
            };
        }

        public static ApiResponse<T> ErrorResponse(string message)
        {
            return new ApiResponse<T>
            {
                Data = default,
                Success = false,
                Message = message
            };
        }
    }
}