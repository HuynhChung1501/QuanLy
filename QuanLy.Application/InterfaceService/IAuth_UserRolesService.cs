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
    public interface IAuth_UserRolesService
    {
        Task<Auth_UserRolesDTODetail> GetByUserID(int userID);
        Task<Auth_UserRolesDTODetail> GetByRoleID(int roleID);
        Task<Auth_UserRolesDTO> Create(Auth_UserRolesDTOCreate account);
        //Task<Auth_UserRolesDTO> Edit(Auth_UserRolesDTOUpdate account);
        //Task<Auth_UserRolesDTOIndex> Search(Auth_UserRolesDTOParam searchParam);
        //Task<string> Delete(string permission);
    }
}
