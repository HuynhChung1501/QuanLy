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
        }
    }
}
