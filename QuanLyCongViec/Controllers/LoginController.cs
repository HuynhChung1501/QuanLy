using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;

namespace QuanLyCongViec.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ILoginService _loginService;
        private readonly IQuanLyRepositoryWrapper _quanLyRepo;
        private readonly DASContext _context;

        public LoginController(IMapper mapper, ILoginService loginService, IQuanLyRepositoryWrapper quanLyRepo, DASContext context)
        {
            _mapper = mapper;
            _loginService = loginService;
            _quanLyRepo = quanLyRepo;
            _context = context;
        }

        [HttpPost]
        [Route("SignIn")]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            var lstUer = await _quanLyRepo.AuthUser.GetAllListAsync();
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
                return Ok(new ApiResponse
                {
                    Message = "Đăng nhập thành công!",
                    Data = _loginService.GenerateToken(user)
                });
            }
        }
    }
}
