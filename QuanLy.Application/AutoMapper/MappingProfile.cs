using AutoMapper;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Domain.Models;
namespace QuanLy.Application.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile() {
        // Entity → DTO
            CreateMap<Auth_Users, Auth_UsersDTO>();
            CreateMap<Auth_Users, Auth_UsersDTODetail>();
            

            // DTO → Entity
            CreateMap<Auth_UsersDTOCreate, Auth_Users>();
            CreateMap<Auth_UsersDTOUpdate, Auth_Users>();

            #region Auth_Permissions
            CreateMap<Auth_Permissions, Auth_PermissionsDTOCreate>();
            CreateMap<Auth_Permissions, Auth_PermissionsDTO>();
            CreateMap<Auth_Permissions, Auth_PermissionsDTODetail>();

            CreateMap<Auth_PermissionsDTOCreate, Auth_Permissions>();
            #endregion

            #region Roles
            CreateMap<Auth_Roles, Auth_RolesDTOCreate>();
            CreateMap<Auth_Roles, Auth_RolesDTO>();
            CreateMap<Auth_Roles, Auth_RolesDTODetail>();
            #endregion
        }
    }
}
