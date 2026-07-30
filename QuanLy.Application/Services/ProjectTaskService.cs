using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLy.Application.DTO;
using QuanLy.Application.DTO.ProjectTasks;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;

namespace QuanLy.Application.Services
{
    public class ProjectTaskService : BaseMasterService, IProjectTaskService
    {
        private readonly IQuanLyRepositoryWrapper _repo; private readonly IMapper _mapper;
        public ProjectTaskService(IQuanLyRepositoryWrapper repo, IMapper mapper) : base(repo) { _repo = repo; _mapper = mapper; }

        public async Task<ProjectTaskDTO> GetByID(int id) => _mapper.Map<ProjectTaskDTO>(await _repo.ProjectTaskRepository.FirstOrDefaultNoTrackingAsync(x => x.TaskID == id && x.IsShow == 1) ?? throw new KeyNotFoundException("Không tìm thấy Task phù hợp."));

        public async Task<PagedResult<ProjectTaskDTO>> Search(ProjectTaskDTOParam p)
        {
            var q = _repo.ProjectTaskRepository.GetAll().AsNoTracking().Where(x => x.IsShow == 1);
            if (!string.IsNullOrWhiteSpace(p.Keyword)) { var k = p.Keyword.Trim().ToLower(); q = q.Where(x => x.Title.ToLower().Contains(k)); }
            if (p.ProjectID.HasValue) q = q.Where(x => x.ProjectID == p.ProjectID.Value);
            if (p.SprintID.HasValue) q = q.Where(x => x.SprintID == p.SprintID.Value);
            if (p.AssigneeID.HasValue) q = q.Where(x => x.AssigneeID == p.AssigneeID.Value);
            if (p.Priority.HasValue) q = q.Where(x => x.Priority == p.Priority.Value);
            if (p.Status.HasValue) q = q.Where(x => x.Status == p.Status.Value);
            var total = await q.CountAsync();
            var items = await q.OrderByDescending(x => x.TaskID).Skip((p.PageIndex - 1) * p.PageSize).Take(p.PageSize).ProjectTo<ProjectTaskDTO>(_mapper.ConfigurationProvider).ToListAsync();
            return new PagedResult<ProjectTaskDTO> { PageIndex = p.PageIndex, PageSize = p.PageSize, TotalRecords = total, TotalPages = (int)Math.Ceiling(total / (double)p.PageSize), Items = items };
        }

        public async Task<ProjectTaskDTO> Create(ProjectTaskDTOCreate model)
        {
            await ValidateRelations(model.ProjectID, model.SprintID);
            var entity = _mapper.Map<ProjectTask>(model); entity.Title = model.Title.Trim(); entity.IsShow = 1; entity.CreatedDate = DateTime.UtcNow;
            await _repo.ProjectTaskRepository.InsertAsync(entity); await _repo.ProjectTaskRepository.SaveChangesAsync(); return _mapper.Map<ProjectTaskDTO>(entity);
        }

        public async Task<ProjectTaskDTO> Edit(ProjectTaskDTOUpdate model)
        {
            var entity = await _repo.ProjectTaskRepository.FirstOrDefaultAsync(x => x.TaskID == model.TaskID) ?? throw new KeyNotFoundException("Không tìm thấy Task phù hợp.");
            await ValidateRelations(model.ProjectID, model.SprintID);
            _mapper.Map(model, entity); entity.Title = model.Title.Trim(); entity.UpdatedDate = DateTime.UtcNow;
            await _repo.ProjectTaskRepository.SaveChangesAsync(); return _mapper.Map<ProjectTaskDTO>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repo.ProjectTaskRepository.FirstOrDefaultAsync(x => x.TaskID == id) ?? throw new KeyNotFoundException("Không tìm thấy Task phù hợp.");
            await _repo.ProjectTaskRepository.DeleteAsync(entity); await _repo.ProjectTaskRepository.SaveChangesAsync(); return true;
        }

        private async Task ValidateRelations(int projectId, int? sprintId)
        {
            if (!await _repo.ProjectRepository.AnyAsync(x => x.ProjectID == projectId && x.IsShow == 1)) throw new AppException("Project không tồn tại.");
            if (sprintId.HasValue)
            {
                var sprint = await _repo.SprintRepository.FirstOrDefaultAsync(x => x.SprintID == sprintId.Value && x.IsShow == 1) ?? throw new AppException("Sprint không tồn tại.");
                if (sprint.ProjectID != projectId) throw new AppException("Sprint không thuộc Project đã chọn.");
                if (sprint.Status == 0) throw new AppException("Không được thêm Task vào Sprint đã đóng.");
            }
        }
    }
}
