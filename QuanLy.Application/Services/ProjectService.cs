using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using QuanLy.Application.DTO;
using QuanLy.Application.DTO.Projects;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;

namespace QuanLy.Application.Services
{
    public class ProjectService : BaseMasterService, IProjectService
    {
        private readonly IQuanLyRepositoryWrapper _repo;
        private readonly IMapper _mapper;

        public ProjectService(IQuanLyRepositoryWrapper repo, IMapper mapper) : base(repo)
        {
            _repo = repo;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> GetByID(int id)
        {
            var entity = await _repo.ProjectRepository.GetAll().AsNoTracking()
                .Include(x => x.Sprints).Include(x => x.Tasks)
                .FirstOrDefaultAsync(x => x.ProjectID == id && x.IsShow == 1)
                ?? throw new KeyNotFoundException("Không tìm thấy Project phù hợp.");
            return ToDto(entity);
        }

        public async Task<PagedResult<ProjectDTO>> Search(ProjectDTOParam param)
        {
            var query = _repo.ProjectRepository.GetAll().AsNoTracking()
                .Include(x => x.Sprints).Include(x => x.Tasks)
                .Where(x => x.IsShow == 1);
            if (!string.IsNullOrWhiteSpace(param.Keyword))
            {
                var keyword = param.Keyword.Trim().ToLower();
                query = query.Where(x => x.Code.ToLower().Contains(keyword) || x.Name.ToLower().Contains(keyword));
            }
            if (param.Status.HasValue) query = query.Where(x => x.Status == param.Status.Value);
            if (param.ProjectManagerID.HasValue) query = query.Where(x => x.ProjectManagerID == param.ProjectManagerID.Value);

            var total = await query.CountAsync();
            var entities = await query.OrderByDescending(x => x.ProjectID)
                .Skip((param.PageIndex - 1) * param.PageSize).Take(param.PageSize).ToListAsync();
            return Page(param.PageIndex, param.PageSize, total, entities.Select(ToDto).ToList());
        }

        public async Task<ProjectDTO> Create(ProjectDTOCreate model)
        {
            ValidateDates(model.StartDate, model.EndDate);
            var code = model.Code.Trim().ToUpperInvariant();
            if (await _repo.ProjectRepository.AnyAsync(x => x.Code.ToUpper() == code))
                throw new DuplicateData($"Mã Project '{code}' đã tồn tại.");

            var entity = _mapper.Map<Project>(model);
            entity.Code = code;
            entity.Name = model.Name.Trim();
            entity.IsShow = 1;
            entity.CreatedDate = DateTime.UtcNow;
            await _repo.ProjectRepository.InsertAsync(entity);
            await _repo.ProjectRepository.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(entity);
        }

        public async Task<ProjectDTO> Edit(ProjectDTOUpdate model)
        {
            ValidateDates(model.StartDate, model.EndDate);
            var entity = await _repo.ProjectRepository.FirstOrDefaultAsync(x => x.ProjectID == model.ProjectID)
                ?? throw new KeyNotFoundException("Không tìm thấy Project phù hợp.");
            var code = model.Code.Trim().ToUpperInvariant();
            if (await _repo.ProjectRepository.AnyAsync(x => x.ProjectID != model.ProjectID && x.Code.ToUpper() == code))
                throw new DuplicateData($"Mã Project '{code}' đã tồn tại.");

            _mapper.Map(model, entity);
            entity.Code = code;
            entity.Name = model.Name.Trim();
            entity.UpdatedDate = DateTime.UtcNow;
            await _repo.ProjectRepository.SaveChangesAsync();
            return _mapper.Map<ProjectDTO>(entity);
        }

        public async Task<bool> Delete(int id)
        {
            var entity = await _repo.ProjectRepository.FirstOrDefaultAsync(x => x.ProjectID == id)
                ?? throw new KeyNotFoundException("Không tìm thấy Project phù hợp.");
            if (await _repo.SprintRepository.AnyAsync(x => x.ProjectID == id) || await _repo.ProjectTaskRepository.AnyAsync(x => x.ProjectID == id))
                throw new AppException("Không thể xóa Project đang có Sprint hoặc Task.");
            await _repo.ProjectRepository.DeleteAsync(entity);
            await _repo.ProjectRepository.SaveChangesAsync();
            return true;
        }

        private ProjectDTO ToDto(Project entity)
        {
            var dto = _mapper.Map<ProjectDTO>(entity);
            dto.SprintCount = entity.Sprints.Count(x => x.IsShow == 1);
            dto.TaskCount = entity.Tasks.Count(x => x.IsShow == 1);
            var visibleTasks = entity.Tasks.Where(x => x.IsShow == 1).ToList();
            dto.Progress = visibleTasks.Count == 0 ? 0 : Math.Round(visibleTasks.Count(x => x.Status == 2) * 100m / visibleTasks.Count, 2);
            return dto;
        }

        private static void ValidateDates(DateTime start, DateTime end)
        {
            if (start >= end) throw new AppException("Ngày bắt đầu phải nhỏ hơn ngày kết thúc.");
        }

        private static PagedResult<T> Page<T>(int index, int size, int total, List<T> items) => new()
        {
            PageIndex = index, PageSize = size, TotalRecords = total,
            TotalPages = (int)Math.Ceiling(total / (double)size), Items = items
        };
    }
}
