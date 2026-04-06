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
    public interface IAuth_AssignService
    {
        Task<Auth_AssignDTODetail> GetObjectID(int id);
        Task<Auth_AssignDTODetail> GetPermission(string permission);
        Task<Auth_AssignDTOIndex> Search(Auth_AssignDTOParam searchParam);
        Task<Auth_AssignDTO> Create(Auth_AssignDTOCreate account);
        Task<Auth_AssignDTO> Edit(Auth_AssignDTOUpdate account);
        Task<string> Delete(int id);
    }
}
