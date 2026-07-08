using QuanLy.Application.InterfaceService;
using QuanLy.Application.Services;
using QuanLy.Domain.Interface;
using QuanLy.Infrastructure.Repositories;

namespace QuanLyCongViec.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            #region Đăng ký Repository
            services.AddScoped<IQuanLyRepositoryWrapper, QuanLyRepositoryWrapper>();
            #endregion

            #region Đăng ký Service

            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<IAuth_UsersService, Auth_UsersService>();
            services.AddScoped<ITokenService, TokenService>();

            #endregion

            return services;
        }
    }
}
