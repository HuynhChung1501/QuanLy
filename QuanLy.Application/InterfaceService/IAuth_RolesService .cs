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
    public interface IAuth_RolesService 
    {
        Task<Auth_RolesDTODetail> GetByID(int id);
        Task<Auth_RolesDTODetail> GetByName(string name);
        Task<Auth_RolesDTOIndex> Search(Auth_RolesDTOParam searchParam);
        Task<Auth_RolesDTO> Create(Auth_RolesDTOCreate account);
        Task<Auth_RolesDTO> Edit(Auth_RolesDTOUpdate account);
        Task<bool> Delete(int id);
    }
}
