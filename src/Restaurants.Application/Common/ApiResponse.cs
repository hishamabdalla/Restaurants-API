using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurants.Application.Common
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }=false;
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
        public ApiResponse(bool success, int statusCode, string message, T? data = default, List<string>? errors = null)
        {
            Success = success;
            Message = message;
            StatusCode = statusCode;
            Data = data;
            Errors = errors ?? new List<string>();

        }
        public ApiResponse(T data)
        {
            Success = true;
            StatusCode=200;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }

    }
}
