using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using QuanLy.Application.DTO.Projects;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Models;

namespace QuanLyCongViec.Controllers
{
    [EnableRateLimiting(RateLimiterExtensions.Api)]
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _service;
        public ProjectController(IProjectService service) { _service = service; }
        [HttpGet("GetID")] public async Task<IActionResult> GetID(int id) => Ok(new ApiResponse { Message = "Lấy Project thành công!", Data = await _service.GetByID(id) });
        [HttpPost("GetList")] public async Task<IActionResult> GetList(ProjectDTOParam param) => Ok(new ApiResponse { Message = "Lấy danh sách Project thành công!", Data = await _service.Search(param) });
        [HttpPost("SearchPagination")] public async Task<IActionResult> SearchPagination(ProjectDTOParam param) => Ok(new ApiResponse { Message = "Lấy danh sách Project thành công!", Data = await _service.Search(param) });
        [HttpPost("Create")] public async Task<IActionResult> Create(ProjectDTOCreate model) => Ok(new ApiResponse { Message = "Tạo Project thành công!", Data = await _service.Create(model) });
        [HttpPut("Update")] public async Task<IActionResult> Update(ProjectDTOUpdate model) => Ok(new ApiResponse { Message = $"Cập nhật Project: {model.Name} thành công!", Data = await _service.Edit(model) });
        [HttpDelete("Delete")] public async Task<IActionResult> Delete(int id) => Ok(new ApiResponse { Message = "Xóa Project thành công!", Data = await _service.Delete(id) });
    }
}
