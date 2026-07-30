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
            services.AddScoped<IAuth_PermissionsService, Auth_PermissionsService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IAuth_RolesService, Auth_RolesService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<ISprintService, SprintService>();
            services.AddScoped<IProjectTaskService, ProjectTaskService>();

            #endregion

            return services;
        }
    }
}
