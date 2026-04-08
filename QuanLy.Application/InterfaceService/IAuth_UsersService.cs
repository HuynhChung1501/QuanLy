using QuanLy.Application.DTO;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.InterfaceService
{
    public interface IAuth_UsersService
    {
        Task<Auth_UsersDTODetail> GetID(int id);
        Task<List<Auth_UsersDTO>> Search(Auth_UsersDTOParam searchParam);
        Task<PagedResult<Auth_UsersDTO>> SearchPagination(Auth_UsersDTOParam searchParam);
        Task<Auth_UsersDTO> Create(Auth_UsersDTOCreate account);
        Task<Auth_UsersDTO> Edit(Auth_UsersDTOUpdate account);
        Task<(bool, string)> Delete(int id);
    }
}
