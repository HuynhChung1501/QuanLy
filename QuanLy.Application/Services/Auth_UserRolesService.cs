using AutoMapper;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
namespace QuanLy.Application.Services
{
    public class Auth_UserRolesService : BaseMasterService, IAuth_UserRolesService
    {
        private readonly IMapper _mapper;
        private readonly IQuanLyRepositoryWrapper _QLContext;
        public Auth_UserRolesService(IQuanLyRepositoryWrapper QuanLyRepository, IMapper mapper) : base(QuanLyRepository)
        {
            _mapper = mapper;
            _QLContext = QuanLyRepository;
        }

        public async Task<Auth_UserRolesDTODetail> GetByUserID(int userID)
        {
            var model = new Auth_UserRolesDTODetail();
            var auth_UserRole = await _QLContext.Auth_UserRolesRepository.FirstOrDefaultAsync(x => x.UserID == userID);
            if (auth_UserRole != null)
            {
                model = _mapper.Map<Auth_UserRolesDTODetail>(auth_UserRole);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_UserRolesDTODetail> GetByRoleID(int roleID)
        {
            var model = new Auth_UserRolesDTODetail();
            var auth_UserRole = await _QLContext.Auth_UserRolesRepository.FirstOrDefaultAsync(x => x.RoleID == roleID);
            if (auth_UserRole != null)
            {
                model = _mapper.Map<Auth_UserRolesDTODetail>(auth_UserRole);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_UserRolesDTO> Create(Auth_UserRolesDTOCreate model)
        {
            try
            {
                Auth_UserRoles auth_UserRole = _mapper.Map<Auth_UserRoles>(model);

                await _QLContext.Auth_UserRolesRepository.InsertAsync(auth_UserRole);
                await _QLContext.Auth_UserRolesRepository.SaveChangesAsync();

                return _mapper.Map<Auth_UserRolesDTO>(model);
                throw new AppException($"Tạo mới thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        //public async Task<string> Delete(string permission)
        //{
        //    try
        //    {
        //        var model = await _QLContext.Auth_UserRolesRepository.FirstOrDefaultAsync(x => x.Permission.Contains(permission));

        //        if (model == null) throw new AppException("Không tìm thấy dữ liệu phù hợp.");

        //        await _QLContext.Auth_UserRolesRepository.DeleteAsync(model);
        //        await _QLContext.Auth_UserRolesRepository.SaveChangesAsync();
        //        throw new AppException($"Xóa thành công");
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AppException(ex.Message);
        //    }
        //}

        //public async Task<Auth_UserRolesDTO> Edit(Auth_UserRolesDTOUpdate model)
        //{
        //    try
        //    {
        //        var auth_UserRole = await _QLContext.Auth_UserRolesRepository.FirstOrDefaultAsync(x => x.Permission.Contains(model.Permission ?? ""));

        //        if (auth_UserRole == null) throw new AppException("Không tìm thấy dữ liệu phù hợp");

        //        auth_UserRole = _mapper.Map<Auth_Permissions>(auth_UserRole);

        //        await _QLContext.Auth_UserRolesRepository.UpdateAsync(auth_UserRole);
        //        await _QLContext.Auth_UserRolesRepository.SaveChangesAsync();

        //        return _mapper.Map<Auth_UserRolesDTO>(auth_UserRole);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AppException(ex.Message);
        //    }
        //}


        #region Tìm kiếm theo userName và số điện thoại
        //public async Task<Auth_UserRolesDTOIndex> Search(Auth_UserRolesDTOParam searchParam)
        //{
        //    try
        //    {
        //        var result = new Auth_UserRolesDTOIndex();
        //        var accounts = await (from a in _QLContext.Auth_UserRolesRepository.GetAll()
        //                              where (!string.IsNullOrEmpty(searchParam.Permission) ? a.Permission.Contains(searchParam.Permission) : true)
        //                              select a).ToListAsync();
        //        var accountResult = new List<Auth_UserRolesDTO>();
        //        foreach (var item in accounts)
        //        {
        //            accountResult.Add(_mapper.Map<Auth_UserRolesDTO>(item));
        //        }
        //        result.Auth_Permissions = accountResult;
        //        if (accounts.Count == 0 || !accounts.Any())
        //        {
        //            throw new KeyNotFoundException("Không tìm thấy dữ liệu phù hợp");
        //        }

        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new AppException(ex.Message);
        //    }
        //}
        #endregion
    }
}
