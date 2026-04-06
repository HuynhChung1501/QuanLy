using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Enums;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;
namespace QuanLy.Application.Services
{
    public class Auth_Assign_roleService : BaseMasterService, IAuth_Assign_roleService
    {
        private readonly IMapper _mapper;
        private readonly IQuanLyRepositoryWrapper _QLContext;
        public Auth_Assign_roleService(IQuanLyRepositoryWrapper QuanLyRepository, IMapper mapper) : base(QuanLyRepository)
        {
            _mapper = mapper;
            _QLContext = QuanLyRepository;
        }

        public async Task<Auth_Assign_roleDTODetail> GetObjectID(int id)
        {
            var model = new Auth_Assign_roleDTODetail();
            var auth_Assign = await _QLContext.Auth_Assign_RoleRepository.FirstOrDefaultAsync(x => x.ObjectID == id);
            if (auth_Assign != null)
            {
                model = _mapper.Map<Auth_Assign_roleDTODetail>(auth_Assign);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_Assign_roleDTODetail> GetPermission(string permission)
        {
            var model = new Auth_Assign_roleDTODetail();
            var auth_Assign = await _QLContext.Auth_Assign_RoleRepository.FirstOrDefaultAsync(x => x.Permission.Contains(permission));
            if (auth_Assign != null)
            {
                model = _mapper.Map<Auth_Assign_roleDTODetail>(auth_Assign);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_Assign_roleDTO> Create(Auth_Assign_roleDTOCreate model)
        {
            try
            {
                if (await _QLContext.Auth_Assign_RoleRepository.AnyAsync(a => a.ObjectID == model.ObjectID && a.Permission == model.Permission))
                {
                    throw new AppException($"Auth_Assign đã tồn tại");
                }
                Auth_Assign_role auth_Assign = _mapper.Map<Auth_Assign_role>(model);

                await _QLContext.Auth_Assign_RoleRepository.InsertAsync(auth_Assign);
                await _QLContext.Auth_Assign_RoleRepository.SaveChangesAsync();

                return _mapper.Map<Auth_Assign_roleDTO>(model);
                throw new AppException($"Tạo mới thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                var model = await _QLContext.Auth_Assign_RoleRepository.FirstOrDefaultAsync(a => a.ObjectID == id );

                if (model == null) throw new AppException("Không tìm thấy dữ liệu phù hợp.");

                await _QLContext.Auth_Assign_RoleRepository.DeleteAsync(model);
                await _QLContext.Auth_Assign_RoleRepository.SaveChangesAsync();
                throw new AppException($"Xóa thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<Auth_Assign_roleDTO> Edit(Auth_Assign_roleDTOUpdate model)
        {
            try
            {
                var auth_Assign = await _QLContext.Auth_Assign_RoleRepository.FirstOrDefaultAsync(a => a.ObjectID == model.ObjectID && a.Permission.Contains(model.Permission ?? ""));

                if (auth_Assign == null) throw new AppException("Không tìm thấy dữ liệu phù hợp");

                auth_Assign = _mapper.Map<Auth_Assign_role>(auth_Assign);

                await _QLContext.Auth_Assign_RoleRepository.UpdateAsync(auth_Assign);
                await _QLContext.Auth_Assign_RoleRepository.SaveChangesAsync();

                return _mapper.Map<Auth_Assign_roleDTO>(auth_Assign);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }


        #region Tìm kiếm theo ObjectId và Permission
        public async Task<Auth_Assign_roleDTOIndex> Search(Auth_Assign_roleDTOParam searchParam)
        {
            try
            {
                var result = new Auth_Assign_roleDTOIndex();
                var accounts = await (from a in _QLContext.Auth_Assign_RoleRepository.GetAll()
                                      where (!string.IsNullOrEmpty(searchParam.Permission) ? a.Permission.Contains(searchParam.Permission) : true)
                                      && (searchParam.ObjectId != null ? a.ObjectID == searchParam.ObjectId : true)
                                      select a).ToListAsync();
                var accountResult = new List<Auth_Assign_role>();
                foreach (var item in accounts)
                {
                    accountResult.Add(_mapper.Map<Auth_Assign_role>(item));
                }
                result.Auth_Assign_roles = accountResult;
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
