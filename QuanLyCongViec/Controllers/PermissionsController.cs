using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Models;

namespace QuanLyCongViec.Controllers
{
    [EnableRateLimiting(RateLimiterExtensions.Api)]
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionsController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAuth_PermissionsService _iAuth_PermissionsService;

        public PermissionsController(IAuth_PermissionsService iAuth_PermissionsService, ILogger<AccountController> logger)
        {
            _iAuth_PermissionsService = iAuth_PermissionsService;
            _logger = logger;
        }

        
        [HttpPost]
        [Route("GetPermission")]
        public async Task<IActionResult> GetPermission(Auth_PermissionsDTOParam searchParam)
        {
            var result = await _iAuth_PermissionsService.Search(searchParam);

            return Ok(new ApiResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = result
            });
        }

        [HttpPost]
        [Route("SearchPagination")]
        public async Task<IActionResult> SearchPagination(Auth_PermissionsDTOParam searchParam)
        {
            var user = await _iAuth_PermissionsService.Search(searchParam);

            return Ok(new ApiResponse
            {
                Message = "Lấy danh sách thành công!",
                Data = user
            });
        }

        //[HttpGet]
        //[Route("GetID")]
        //public async Task<IActionResult> GetID(int id)
        //{
        //    var user = await _iAuth_PermissionsService.GetID(id);

        //    return Ok(new ApiResponse
        //    {
        //        Message = "Lấy user thành công!",
        //        Data = user
        //    });
        //}

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Auth_PermissionsDTOCreate model)
        {
            var user = await _iAuth_PermissionsService.Create(model);

            return Ok(new ApiResponse
            {
                Message = "Tạo mới thành công!",
                Data = user
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Auth_PermissionsDTOUpdate model)
        {
            try
            {
                
                var user = await _iAuth_PermissionsService.Edit(model);

                return Ok(new ApiResponse
                {
                    Message = $"Cập nhật Permission: {model.Permission} thành công!",
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

        //[HttpDelete]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(string name)
        //{
        //    try
        //    {
        //        var isDeleted = await _iAuth_PermissionsService.Delete(name);

        //        return Ok(new ApiResponse
        //        {
        //            Success = isDeleted.Item1,
        //            Message = isDeleted.Item2,
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Exception when deleting user {UserId}", name);
        //        return StatusCode(500, new ApiResponse
        //        {
        //            Message = "Có lỗi xảy ra"
        //        });
        //    }
        //}
    }
}
