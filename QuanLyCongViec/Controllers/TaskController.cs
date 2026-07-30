using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using QuanLy.Application.DTO.ProjectTasks;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Models;

namespace QuanLyCongViec.Controllers
{
    [EnableRateLimiting(RateLimiterExtensions.Api)]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IProjectTaskService _service;
        public TaskController(IProjectTaskService service) { _service = service; }
        [HttpGet("GetID")] public async System.Threading.Tasks.Task<IActionResult> GetID(int id) => Ok(new ApiResponse { Message = "Lấy Task thành công!", Data = await _service.GetByID(id) });
        [HttpPost("GetList")] public async System.Threading.Tasks.Task<IActionResult> GetList(ProjectTaskDTOParam param) => Ok(new ApiResponse { Message = "Lấy danh sách Task thành công!", Data = await _service.Search(param) });
        [HttpPost("SearchPagination")] public async System.Threading.Tasks.Task<IActionResult> SearchPagination(ProjectTaskDTOParam param) => Ok(new ApiResponse { Message = "Lấy danh sách Task thành công!", Data = await _service.Search(param) });
        [HttpPost("Create")] public async System.Threading.Tasks.Task<IActionResult> Create(ProjectTaskDTOCreate model) => Ok(new ApiResponse { Message = "Tạo Task thành công!", Data = await _service.Create(model) });
        [HttpPut("Update")] public async System.Threading.Tasks.Task<IActionResult> Update(ProjectTaskDTOUpdate model) => Ok(new ApiResponse { Message = $"Cập nhật Task: {model.Title} thành công!", Data = await _service.Edit(model) });
        [HttpDelete("Delete")] public async System.Threading.Tasks.Task<IActionResult> Delete(int id) => Ok(new ApiResponse { Message = "Xóa Task thành công!", Data = await _service.Delete(id) });
    }
}
