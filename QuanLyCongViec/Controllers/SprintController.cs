using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using QuanLy.Application.DTO.Sprints;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Models;

namespace QuanLyCongViec.Controllers
{
    [EnableRateLimiting(RateLimiterExtensions.Api)]
    [Route("api/[controller]")]
    [ApiController]
    public class SprintController : ControllerBase
    {
        private readonly ISprintService _service;
        public SprintController(ISprintService service) { _service = service; }
        [HttpGet("GetID")] public async Task<IActionResult> GetID(int id) => Ok(new ApiResponse { Message = "Lấy Sprint thành công!", Data = await _service.GetByID(id) });
        [HttpPost("GetList")] public async Task<IActionResult> GetList(SprintDTOParam param) => Ok(new ApiResponse { Message = "Lấy danh sách Sprint thành công!", Data = await _service.Search(param) });
        [HttpPost("SearchPagination")] public async Task<IActionResult> SearchPagination(SprintDTOParam param) => Ok(new ApiResponse { Message = "Lấy danh sách Sprint thành công!", Data = await _service.Search(param) });
        [HttpPost("Create")] public async Task<IActionResult> Create(SprintDTOCreate model) => Ok(new ApiResponse { Message = "Tạo Sprint thành công!", Data = await _service.Create(model) });
        [HttpPut("Update")] public async Task<IActionResult> Update(SprintDTOUpdate model) => Ok(new ApiResponse { Message = $"Cập nhật Sprint: {model.Name} thành công!", Data = await _service.Edit(model) });
        [HttpDelete("Delete")] public async Task<IActionResult> Delete(int id) => Ok(new ApiResponse { Message = "Xóa Sprint thành công!", Data = await _service.Delete(id) });
    }
}
