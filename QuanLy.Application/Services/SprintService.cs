using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLy.Application.DTO;
using QuanLy.Application.DTO.Sprints;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;

namespace QuanLy.Application.Services
{
    public class SprintService : BaseMasterService, ISprintService
    {
        private readonly IQuanLyRepositoryWrapper _repo;
        private readonly IMapper _mapper;
        public SprintService(IQuanLyRepositoryWrapper repo, IMapper mapper) : base(repo) { _repo = repo; _mapper = mapper; }

        public async Task<SprintDTO> GetByID(int id)
        {
            var entity = await _repo.SprintRepository.GetAll().AsNoTracking().Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.SprintID == id && x.IsShow == 1)
                ?? throw new KeyNotFoundException("Không tìm thấy Sprint phù hợp.");
            return ToDto(entity);
        }

        public async Task<PagedResult<SprintDTO>> Search(SprintDTOParam param)
        {
            var query = _repo.SprintRepository.GetAll().AsNoTracking().Include(x => x.Tasks).Where(x => x.IsShow == 1);
            if (!string.IsNullOrWhiteSpace(param.Keyword)) { var k = param.Keyword.Trim().ToLower(); query = query.Where(x => x.Name.ToLower().Contains(k)); }
            if (param.ProjectID.HasValue) query = query.Where(x => x.ProjectID == param.ProjectID.Value);
            if (param.Status.HasValue) query = query.Where(x => x.Status == param.Status.Value);
            var total = await query.CountAsync();
            var data = await query.OrderByDescending(x => x.SprintID).Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize).ToListAsync();
            return Page(param, total, data.Select(ToDto).ToList());
        }

        public async Task<SprintDTO> Create(SprintDTOCreate model)
        {
            await Validate(model.ProjectID, model.StartDate, model.EndDate, model.Status, null);
            var entity = _mapper.Map<Sprint>(model);
            entity.Name = model.Name.Trim(); entity.IsShow = 1; entity.CreatedDate = DateTime.UtcNow;
            await _repo.SprintRepository.InsertAsync(entity); await _repo.SprintRepository.SaveChangesAsync();
            return _mapper.Map<SprintDTO>(entity);
        }

        public async Task<SprintDTO> Edit(SprintDTOUpdate model)
        {
            var entity = await _repo.SprintRepository.FirstOrDefaultAsync(x => x.SprintID == model.SprintID)
                ?? throw new KeyNotFoundException("Không tìm thấy Sprint phù hợp.");
            await Validate(model.ProjectID, model.StartDate, model.EndDate, model.Status, model.SprintID);
            _mapper.Map(model, entity); entity.Name = model.Name.Trim(); entity.UpdatedDate = DateTime.UtcNow;
            await _repo.SprintRepository.SaveChangesAsync(); return _mapper.Map<SprintDTO>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repo.SprintRepository.FirstOrDefaultAsync(x => x.SprintID == id)
                ?? throw new KeyNotFoundException("Không tìm thấy Sprint phù hợp.");
            if (await _repo.ProjectTaskRepository.AnyAsync(x => x.SprintID == id)) throw new AppException("Không thể xóa Sprint đang có Task.");
            await _repo.SprintRepository.DeleteAsync(entity); await _repo.SprintRepository.SaveChangesAsync(); return true;
        }

        private async Task Validate(int projectId, DateTime start, DateTime end, int status, int? sprintId)
        {
            if (start >= end) throw new AppException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
            var project = await _repo.ProjectRepository.FirstOrDefaultAsync(x => x.ProjectID == projectId && x.IsShow == 1)
                ?? throw new AppException("Project không tồn tại.");
            if (start < project.StartDate || end > project.EndDate) throw new AppException("Thời gian Sprint phải nằm trong thời gian của Project.");
            if (status == 1 && await _repo.SprintRepository.AnyAsync(x => x.ProjectID == projectId && x.Status == 1 && x.IsShow == 1 && (!sprintId.HasValue || x.SprintID != sprintId.Value)))
                throw new AppException("Một Project chỉ được có một Sprint đang hoạt động.");
        }

        private SprintDTO ToDto(Sprint entity)
        {
            var dto = _mapper.Map<SprintDTO>(entity); var tasks = entity.Tasks.Where(x => x.IsShow == 1).ToList();
            dto.TaskCount = tasks.Count; dto.Progress = tasks.Count == 0 ? 0 : Math.Round(tasks.Count(x => x.Status == 2) * 100m / tasks.Count, 2); return dto;
        }
        private static PagedResult<SprintDTO> Page(SprintDTOParam p, int total, List<SprintDTO> items) => new() { PageIndex = p.PageIndex, PageSize = p.PageSize, TotalRecords = total, TotalPages = (int)Math.Ceiling(total / (double)p.PageSize), Items = items };
    }
}
