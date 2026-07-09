using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.DependencyInjection;
using QuanLy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.RateLimiting;
using System.Threading.Tasks;

namespace QuanLy.Application.Helpers
{
    public static class RateLimiterExtensions
    {
        public const string Login = "Login";

        public const string Refresh = "Refresh";

        public const string Upload = "Upload";

        public const string Api = "Api";
        public static IServiceCollection AddRateLimiterConfiguration(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";

                    await context.HttpContext.Response.WriteAsJsonAsync(new ApiResponse
                    {
                        Success = false,
                        StatusCode = StatusCodes.Status429TooManyRequests,
                        Message = "Bạn đã gửi quá nhiều yêu cầu. Vui lòng thử lại sau."
                    }, token);
                };

                options.AddFixedWindowLimiter(Login, opt =>
                {
                    opt.PermitLimit = 5;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueLimit = 0;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.AddFixedWindowLimiter("Refresh", opt =>
                {
                    opt.PermitLimit = 10;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueLimit = 0;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.AddFixedWindowLimiter("Upload", opt =>
                {
                    opt.PermitLimit = 20;
                    opt.Window = TimeSpan.FromMinutes(1);
                    opt.QueueLimit = 0;
                    opt.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                });

                options.AddPolicy("Api", httpContext =>
                {
                    var userId = httpContext.User.FindFirst("UserID")?.Value;

                    return RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: userId ?? "Anonymous",
                        factory: key => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 300,
                            Window = TimeSpan.FromMinutes(1),
                            QueueLimit = 0
                        });
                });
            });

            return services;
        }
    }
}
