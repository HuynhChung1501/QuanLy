using QuanLy.Application.DTO;
using QuanLy.Application.DTO.Sprints;

namespace QuanLy.Application.InterfaceService
{
    public interface ISprintService
    {
        Task<SprintDTO> GetByID(int id);
        Task<PagedResult<SprintDTO>> Search(SprintDTOParam param);
        Task<SprintDTO> Create(SprintDTOCreate model);
        Task<SprintDTO> Edit(SprintDTOUpdate model);
        Task<bool> Delete(int id);
    }
}
