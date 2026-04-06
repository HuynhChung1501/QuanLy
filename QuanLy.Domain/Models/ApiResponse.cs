using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Domain.Models
{
    public class ApiResponse
    {
        public int StatusCode { get; set; } = 200; // HTTP status code
        public bool Success { get; set; } = true;
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}
