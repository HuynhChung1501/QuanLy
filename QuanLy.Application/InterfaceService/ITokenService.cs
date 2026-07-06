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
    public interface ITokenService
    {
        bool InsertToken(int id, string token);
    }
}
