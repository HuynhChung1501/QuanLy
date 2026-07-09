using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;

namespace QuanLyCongViec.Controllers
{
    //[Authorize]
    [EnableRateLimiting(RateLimiterExtensions.Api)]
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAuth_UsersService _iAuth_UsersService;

        public AccountController(IAuth_UsersService iAuth_UsersService, ILogger<AccountController> logger)
        {
            _iAuth_UsersService = iAuth_UsersService;
            _logger = logger;
        }

        
        [HttpPost]
        [AllowAnonymous]
        [Route("GetList")]
        public async Task<IActionResult> GetList(Auth_UsersDTOParam searchParam)
        {
            var user = await _iAuth_UsersService.Search(searchParam);

            return Ok(new ApiResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = user
            });
        }

        [HttpPost]
        [Route("SearchPagination")]
        public async Task<IActionResult> SearchPagination(Auth_UsersDTOParam searchParam)
        {
            var user = await _iAuth_UsersService.Search(searchParam);

            return Ok(new ApiResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = user
            });
        }

        [HttpGet]
        [Route("GetID")]
        public async Task<IActionResult> GetID(int id)
        {
            var user = await _iAuth_UsersService.GetID(id);

            return Ok(new ApiResponse
            {
                Message = "Lấy user thành công!",
                Data = user
            });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Auth_UsersDTOCreate model)
        {
            var user = await _iAuth_UsersService.Create(model);

            return Ok(new ApiResponse
            {
                Message = "Tạo mới thành công!",
                Data = user
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Auth_UsersDTOUpdate model)
        {
            try
            {
                
                var user = await _iAuth_UsersService.Edit(model);

                return Ok(new ApiResponse
                {
                    Message = $"Cập nhật user: {model.UsereName} thành công!",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{GetType()} ex => {ex.Message} ", model);
                return StatusCode(500, new ApiResponse
                {
                    Message = "Có lỗi xảy ra"
                });
            }
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var isDeleted = await _iAuth_UsersService.Delete(id);

                return Ok(new ApiResponse
                {
                    Success = isDeleted.Item1,
                    Message = isDeleted.Item2,
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception when deleting user {UserId}", id);
                return StatusCode(500, new ApiResponse
                {
                    Message = "Có lỗi xảy ra"
                });
            }
        }
    }
}
