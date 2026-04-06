using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Domain.Models;

namespace QuanLy.Application.InterfaceService
{
    public interface IAuth_PermissionsService
    {
        Task<Auth_PermissionsDTODetail> GetPermission(string permission);
        Task<Auth_PermissionsDTOIndex> Search(Auth_PermissionsDTOParam searchParam);
        Task<Auth_PermissionsDTO> Create(Auth_PermissionsDTOCreate account);
        Task<Auth_PermissionsDTO> Edit(Auth_PermissionsDTOUpdate account);
        Task<string> Delete(string permission);
    }
}
