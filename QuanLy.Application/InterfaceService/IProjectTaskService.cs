using QuanLy.Application.DTO;
using QuanLy.Application.DTO.ProjectTasks;

namespace QuanLy.Application.InterfaceService
{
    public interface IProjectTaskService
    {
        Task<ProjectTaskDTO> GetByID(int id);
        Task<PagedResult<ProjectTaskDTO>> Search(ProjectTaskDTOParam param);
        Task<ProjectTaskDTO> Create(ProjectTaskDTOCreate model);
        Task<ProjectTaskDTO> Edit(ProjectTaskDTOUpdate model);
        Task<bool> Delete(int id);
    }
}
