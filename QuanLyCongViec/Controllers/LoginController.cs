using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;
using System.Net.WebSockets;

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
                token.RefreshToken = _loginService.HashRefreshToken(token.RefreshToken);
                bool isInserted = _tokenService.InsertToken(user.UserID, token.RefreshToken);
                if (!isInserted) {
                    return Ok(new ApiResponse
                    {
                        Message = "Đăng nhập không thành công do không lưu được Token!",
                        Data = token
                    });
                }
                return Ok(new ApiResponse
                {
                    Message = "Đăng nhập thành công!",
                    Data = token
                });
            }
        }
    }
}
