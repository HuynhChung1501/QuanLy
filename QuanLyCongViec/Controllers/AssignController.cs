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
    public class AssignController : ControllerBase
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IAuth_PermissionsService _iAuth_PermissionsService;
        private readonly IAuth_AssignService _auth_AssignService;

        public AssignController(IAuth_PermissionsService iAuth_PermissionsService, IAuth_RolesService iAuth_RolesService, IAuth_AssignService Auth_AssignService, ILogger<AccountController> logger)
        {
            _logger = logger;
            _iAuth_PermissionsService = iAuth_PermissionsService;
            _auth_AssignService = Auth_AssignService;
        }

        
        //[HttpPost]
        //[Route("GetList")]
        //public async Task<IActionResult> GetList(Auth_AssignDTOParam param)
        //{
        //    var result = await _auth_AssignService.Search(param);

        //    return Ok(new ApiResponse
        //    {
        //        Message = "Lấy data thành công!",
        //        Data = result
        //    });
        //}

        //[HttpPost]
        //[Route("SearchPagination")]
        //public async Task<IActionResult> SearchPagination(Auth_AssignDTOParam searchParam)
        //{
        //    var result = await _auth_AssignService.Search(searchParam);

        //    return Ok(new ApiResponse
        //    {
        //        Message = "Lấy danh sách thành công!",
        //        Data = result
        //    });
        //}

        //[HttpGet]
        //[Route("GetID")]
        //public async Task<IActionResult> GetID(int id)
        //{
        //    var result = await _auth_AssignService.GetObjectID(id);

        //    return Ok(new ApiResponse
        //    {
        //        Message = "Lấy data thành công!",
        //        Data = result
        //    });
        //}

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> Create(Auth_AssignDTOCreate model)
        {
            var result = await _auth_AssignService.Create(model);

            return Ok(new ApiResponse
            {
                Message = "Tạo mới thành công!",
                Data = result
            });
        }
        /// <summary>
        /// Gán Role cho người dùng
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("AssignUserRole")]
        public async Task<IActionResult> AssignUserRole(Auth_AssignDTOCreate model)
        {
            var result = await _auth_AssignService.Create(model);

            return Ok(new ApiResponse
            {
                Message = "Tạo mới thành công!",
                Data = result
            });
        }

        [HttpPut]
        [Route("Update")]
        public async Task<IActionResult> Update(Auth_AssignDTOUpdate model)
        {
            try
            {
                var user = await _auth_AssignService.Edit(model);

                return Ok(new ApiResponse
                {
                    Message = $"Cập nhật Assgin: {model.Permission} thành công!",
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

        //[HttpDelete]
        //[Route("Delete")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    try
        //    {
        //        var isDeleted = await _auth_AssignService.Delete(id);

        //        return Ok(new ApiResponse
        //        {
        //            Success = isDeleted,
        //            Message = isDeleted ? "Xóa thành công" : "Xóa không thành công",
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError(ex, "Exception when deleting user {UserId}", id);
        //        return StatusCode(500, new ApiResponse
        //        {
        //            Message = "Có lỗi xảy ra"
        //        });
        //    }
        //}
    }
}
