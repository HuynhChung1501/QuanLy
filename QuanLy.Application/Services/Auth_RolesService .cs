using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Enums;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
namespace QuanLy.Application.Services
{
    public class Auth_RolesService : BaseMasterService, IAuth_RolesService
    {
        private readonly IMapper _mapper;
        private readonly IQuanLyRepositoryWrapper _QLContext;
        public Auth_RolesService(IQuanLyRepositoryWrapper QuanLyRepository, IMapper mapper) : base(QuanLyRepository)
        {
            _mapper = mapper;
            _QLContext = QuanLyRepository;
        }

        public async Task<Auth_RolesDTODetail> GetByID(int id)
        {
            var model = new Auth_RolesDTODetail();
            var authRole = await _QLContext.Auth_RolesRepository.FirstOrDefaultAsync(x => x.RoleID == id && x.IsShow == (byte)EnumCommon.Status.Active);
            if (authRole != null)
            {
                model = _mapper.Map<Auth_RolesDTODetail>(authRole);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_RolesDTODetail> GetByName(string name)
        {
            var model = new Auth_RolesDTODetail();
            var authRole = await _QLContext.Auth_RolesRepository.FirstOrDefaultAsync(x => x.Name.Contains(name) && x.IsShow == (byte)EnumCommon.Status.Active);
            if (authRole != null)
            {
                model = _mapper.Map<Auth_RolesDTODetail>(authRole);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_RolesDTO> Create(Auth_RolesDTOCreate model)
        {
            try
            {
                if (await _QLContext.Auth_RolesRepository.AnyAsync(x => x.RoleID == model.RoleID))
                {
                    throw new AppException($"Role: {model.RoleID} đã tồn tại");
                }
                Auth_Roles authRole = _mapper.Map<Auth_Roles>(model);

                await _QLContext.Auth_RolesRepository.InsertAsync(authRole);
                await _QLContext.Auth_RolesRepository.SaveChangesAsync();

                return _mapper.Map<Auth_RolesDTO>(model);
                throw new AppException($"Tạo mới thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<bool> Delete(int id)
        {
            try
            {
                var model = await _QLContext.Auth_RolesRepository.FirstOrDefaultAsync(x => x.RoleID == id);

                if (model == null) throw new AppException("Không tìm thấy dữ liệu phù hợp.");

                await _QLContext.Auth_RolesRepository.DeleteAsync(model);
                await _QLContext.Auth_RolesRepository.SaveChangesAsync();
                throw new AppException($"Xóa thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<Auth_RolesDTO> Edit(Auth_RolesDTOUpdate model)
        {
            try
            {
                var authRole = await _QLContext.Auth_RolesRepository.FirstOrDefaultAsync(x => x.RoleID == model.RoleID);

                if (authRole == null) throw new AppException("Không tìm thấy dữ liệu phù hợp");

                authRole = _mapper.Map<Auth_Roles>(authRole);

                await _QLContext.Auth_RolesRepository.UpdateAsync(authRole);
                await _QLContext.Auth_RolesRepository.SaveChangesAsync();

                return _mapper.Map<Auth_RolesDTO>(authRole);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }


        #region Tìm kiếm theo userName và số điện thoại
        public async Task<Auth_RolesDTOIndex> Search(Auth_RolesDTOParam searchParam)
        {
            try
            {
                var result = new Auth_RolesDTOIndex();
                var roles = await (from a in _QLContext.Auth_RolesRepository.GetAll()
                                      where (!string.IsNullOrEmpty(searchParam.Name) ? a.Name.Contains(searchParam.Name) : true) &&  (a.IsShow == (byte)EnumCommon.Status.Active)
                                      select a).ToListAsync();
                var accountResult = new List<Auth_RolesDTO>();
                if (roles.Count == 0 || !roles.Any())
                {
                    throw new KeyNotFoundException("Không tìm thấy dữ liệu phù hợp");
                }
                foreach (var item in roles)
                {
                    accountResult.Add(_mapper.Map<Auth_RolesDTO>(item));
                }
                result.AuthRoles = accountResult;

                return result;
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }
        #endregion
    }
}
