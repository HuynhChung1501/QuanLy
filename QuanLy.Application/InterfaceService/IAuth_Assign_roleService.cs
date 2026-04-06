using QuanLy.Application.DTO.Auth_Assign;

namespace QuanLy.Application.InterfaceService
{
    public interface IAuth_Assign_roleService
    {
        Task<Auth_Assign_roleDTODetail> GetObjectID(int id);
        Task<Auth_Assign_roleDTODetail> GetPermission(string permission);
        Task<Auth_Assign_roleDTOIndex> Search(Auth_Assign_roleDTOParam searchParam);
        Task<Auth_Assign_roleDTO> Create(Auth_Assign_roleDTOCreate account);
        Task<Auth_Assign_roleDTO> Edit(Auth_Assign_roleDTOUpdate account);
        Task<string> Delete(int id);
    }
}
