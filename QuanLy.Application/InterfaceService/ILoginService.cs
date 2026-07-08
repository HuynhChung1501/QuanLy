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
    public interface ILoginService
    {
        LoginResponse GenerateTokens(Auth_Users acount);
        string GenerateRefreshToken();
        string HashToken(string refreshToken);
        Task<ApiResponse> RefreshToken(RefreshTokenRequest RefreshToken);
    }
}
