using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuanLy.Application.Helpers
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;

        public ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                switch (error)
                {
                    case AppException e:
                        // custom application error
                        // Yêu cầu không hợp lệ, thường do dữ liệu đầu vào sai hoặc thiếu
                        //status code 400
                        response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;
                    case KeyNotFoundException e:
                        // not found error
                        // Tài nguyên được yêu cầu không tồn tại
                        // status code 404
                        response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    case DuplicateData e:
                        // Xung đột trong yêu cầu, chẳng hạn khi cố gắng tạo tài nguyên trùng lặp
                        // Ví dụ: Đăng ký một tài khoản với email đã tồn tại.
                        // status code 409
                        response.StatusCode = (int)HttpStatusCode.Conflict;
                        break;
                    default:
                        // unhandled error
                        _logger.LogError(error, error.Message);
                        response.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                var result = JsonSerializer.Serialize(new { message = error?.Message });
                await response.WriteAsync(result);
            }
        }
    }
}
