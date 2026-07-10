using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Application.Services;
using QuanLy.Domain.Models;

namespace QuanLyCongViec.Controllers
{
    [EnableRateLimiting(RateLimiterExtensions.Api)]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAuth_PermissionsService _iAuth_PermissionsService;
        private readonly IAuth_RolesService _auth_RolesService;

        public RoleController(IAuth_PermissionsService iAuth_PermissionsService, IAuth_RolesService iAuth_RolesService,  ILogger<AccountController> logger)
        {
            _logger = logger;
            _iAuth_PermissionsService = iAuth_PermissionsService;
            _auth_RolesService = iAuth_RolesService;
        }

        
        [HttpGet]
        [Route("GetByName")]
        public async Task<IActionResult> GetByName(string name)
        {
            var result = await _auth_RolesService.GetByName(name);

            return Ok(new ApiResponse
            {
                Message = "Lấy data thành công!",
                Data = result
            });
        }

        [HttpPost]
        [Route("SearchPagination")]
        public async Task<IActionResult> SearchPagination(Auth_RolesDTOParam searchParam)
        {
            var result = await _auth_RolesService.Search(searchParam);

            return Ok(new ApiResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = result
            });
        }

        [HttpGet]
        [Route("GetID")]
        public async Task<IActionResult> GetID(int id)
        {
            var user = await _auth_RolesService.GetByID(id);

            return Ok(new ApiResponse
            {
                Message = "Lấy user thành công!",
                Data = user
            });
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Auth_RolesDTOCreate model)
        {
            var result = await _auth_RolesService.Create(model);

            return Ok(new ApiResponse
            {
                Message = "Tạo mới thành công!",
                Data = result
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Auth_RolesDTOUpdate model)
        {
            try
            {
                var user = await _auth_RolesService.Edit(model);

                return Ok(new ApiResponse
                {
                    Message = $"Cập nhật Role: {model.Name} thành công!",
                    Data = user
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"{GetType()} ex => {ex.Message} ", model);
                return BadRequest(new ApiResponse
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
                var isDeleted = await _auth_RolesService.Delete(id);

                return Ok(new ApiResponse
                {
                    Success = isDeleted,
                    Message = isDeleted ? "Xóa thành công" : "Xóa không thành công",
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
