using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
namespace QuanLy.Application.Services
{
    public class Auth_PermissionsService : BaseMasterService, IAuth_PermissionsService
    {
        private readonly IMapper _mapper;
        private readonly IQuanLyRepositoryWrapper _QLContext;
        public Auth_PermissionsService(IQuanLyRepositoryWrapper QuanLyRepository, IMapper mapper) : base(QuanLyRepository)
        {
            _mapper = mapper;
            _QLContext = QuanLyRepository;
        }

        public async Task<Auth_PermissionsDTODetail> GetPermission(string permission)
        {
            var model = new Auth_PermissionsDTODetail();
            var auth_Users = await _QLContext.Auth_PermissionsRepository.FirstOrDefaultAsync(x => x.Permission.Contains(permission));
            if (auth_Users != null)
            {
                model = _mapper.Map<Auth_PermissionsDTODetail>(auth_Users);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_PermissionsDTO> Create(Auth_PermissionsDTOCreate model)
        {
            try
            {
                if (await _QLContext.Auth_PermissionsRepository.AnyAsync(x => x.Permission.Contains(model.Permission ?? "")))
                {
                    throw new AppException($"Permission: {model.Permission} đã tồn tại");
                }
                Auth_Permissions auth_Users = _mapper.Map<Auth_Permissions>(model);

                await _QLContext.Auth_PermissionsRepository.InsertAsync(auth_Users);
                await _QLContext.Auth_PermissionsRepository.SaveChangesAsync();

                return _mapper.Map<Auth_PermissionsDTO>(model);
                throw new AppException($"Tạo mới thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<string> Delete(string permission)
        {
            try
            {
                var model = await _QLContext.Auth_PermissionsRepository.FirstOrDefaultAsync(x => x.Permission.Contains(permission));

                if (model == null) throw new AppException("Không tìm thấy dữ liệu phù hợp.");

                await _QLContext.Auth_PermissionsRepository.DeleteAsync(model);
                await _QLContext.Auth_PermissionsRepository.SaveChangesAsync();
                throw new AppException($"Xóa thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<Auth_PermissionsDTO> Edit(Auth_PermissionsDTOUpdate model)
        {
            try
            {
                var auth_Users = await _QLContext.Auth_PermissionsRepository.FirstOrDefaultAsync(x => x.Permission.Contains(model.Permission ?? ""));

                if (auth_Users == null) throw new AppException("Không tìm thấy dữ liệu phù hợp");

                auth_Users = _mapper.Map<Auth_Permissions>(auth_Users);

                await _QLContext.Auth_PermissionsRepository.UpdateAsync(auth_Users);
                await _QLContext.Auth_PermissionsRepository.SaveChangesAsync();

                return _mapper.Map<Auth_PermissionsDTO>(auth_Users);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }


        #region Tìm kiếm theo userName và số điện thoại
        public async Task<Auth_PermissionsDTOIndex> Search(Auth_PermissionsDTOParam searchParam)
        {
            try
            {
                var result = new Auth_PermissionsDTOIndex();
                var accounts = await (from a in _QLContext.Auth_PermissionsRepository.GetAll()
                                      where (!string.IsNullOrEmpty(searchParam.Permission) ? a.Permission.Contains(searchParam.Permission) : true)
                                      select a).ToListAsync();
                var accountResult = new List<Auth_PermissionsDTO>();
                foreach (var item in accounts)
                {
                    accountResult.Add(_mapper.Map<Auth_PermissionsDTO>(item));
                }
                result.Auth_Permissions = accountResult;
                if (accounts.Count == 0 || !accounts.Any())
                {
                    throw new KeyNotFoundException("Không tìm thấy dữ liệu phù hợp");
                }

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
