using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;
using System.Net.WebSockets;
using System.Text;

namespace QuanLyCongViec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        private readonly IQuanLyRepositoryWrapper _quanLyRepo;
        private readonly ITokenService _tokenService;
        private readonly DASContext _context;

        public LoginController(IMapper mapper, ILoginService loginService, IQuanLyRepositoryWrapper quanLyRepo, ITokenService tokenService, DASContext context)
        {
            _mapper = mapper;
            _loginService = loginService;
            _quanLyRepo = quanLyRepo;
            _tokenService = tokenService;
            _context = context;
        }

        [EnableRateLimiting(RateLimiterExtensions.Login)]
        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            var user = await _quanLyRepo.AuthUser.SingleOrDefaultAsync(u => u.UsereName.ToLower() == model.UsereName.ToLower() && u.PassWord == model.PassWord);
            if (user == null)
            {
                return Ok(new ApiResponse
                {
                    Message = "UserName, Password không chính xác!",
                });
            }
            else
            {
                var token = _loginService.GenerateTokens(user);
                var hashToken = _loginService.HashToken(token.RefreshToken);
                bool isInserted = await _tokenService.InsertToken(user.UserID, hashToken);
                if (!isInserted)
                {
                    return Ok(new ApiResponse
                    {
                        Message = "Đăng nhập không thành công do không lưu được Token!",
                        Success = false,
                    });
                }
                return Ok(new ApiResponse
                {
                    Message = "Đăng nhập thành công!",
                    Data = token
                });
            }
        }
        [HttpPost]
        [Route("RefreshToken")]
        public async Task<IActionResult> RefreshToken(RefreshTokenRequest RefreshToken)
        {
            var response = await _loginService.RefreshAccessToken(RefreshToken);
            return Ok(response);
        }
    }
}
