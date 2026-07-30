using QuanLy.Application.DTO;
using QuanLy.Application.DTO.Projects;

namespace QuanLy.Application.InterfaceService
{
    public interface IProjectService
    {
        Task<ProjectDTO> GetByID(int id);
        Task<PagedResult<ProjectDTO>> Search(ProjectDTOParam param);
        Task<ProjectDTO> Create(ProjectDTOCreate model);
        Task<ProjectDTO> Edit(ProjectDTOUpdate model);
        Task<bool> Delete(int id);
    }
}
