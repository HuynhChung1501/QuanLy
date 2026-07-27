using AutoMapper;
using Microsoft.EntityFrameworkCore;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
namespace QuanLy.Application.Services
{
    public class Auth_AssignService : BaseMasterService, IAuth_AssignService
    {
        private readonly IMapper _mapper;
        private readonly IQuanLyRepositoryWrapper _QLContext;
        public Auth_AssignService(IQuanLyRepositoryWrapper QuanLyRepository, IMapper mapper) : base(QuanLyRepository)
        {
            _mapper = mapper;
            _QLContext = QuanLyRepository;
        }

        public async Task<Auth_AssignDTODetail> GetObjectID(int id)
        {
            var model = new Auth_AssignDTODetail();
            var auth_Assign = await _QLContext.Auth_AssignRepository.FirstOrDefaultAsync(x => x.ObjectID == id);
            if (auth_Assign != null)
            {
                model = _mapper.Map<Auth_AssignDTODetail>(auth_Assign);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_AssignDTODetail> GetPermission(string permission)
        {
            var model = new Auth_AssignDTODetail();
            var auth_Assign = await _QLContext.Auth_AssignRepository.FirstOrDefaultAsync(x => x.Permission.Contains(permission));
            if (auth_Assign != null)
            {
                model = _mapper.Map<Auth_AssignDTODetail>(auth_Assign);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_AssignDTO> Create(Auth_AssignDTOCreate model)
        {
            try
            {
                if (await _QLContext.Auth_AssignRepository.AnyAsync(a => a.ObjectID == model.ObjectID && a.Permission == model.Permission))
                {
                    throw new AppException($"Auth_Assign đã tồn tại");
                }
                Auth_Assign auth_Assign = _mapper.Map<Auth_Assign>(model);

                await _QLContext.Auth_AssignRepository.InsertAsync(auth_Assign);
                await _QLContext.Auth_AssignRepository.SaveChangesAsync();

                return _mapper.Map<Auth_AssignDTO>(model);
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
                var model = await _QLContext.Auth_AssignRepository.FirstOrDefaultAsync(a => a.ObjectID == id );

                if (model == null) throw new AppException("Không tìm thấy dữ liệu phù hợp.");

                await _QLContext.Auth_AssignRepository.DeleteAsync(model);
                await _QLContext.Auth_AssignRepository.SaveChangesAsync();
                throw new AppException($"Xóa thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<Auth_AssignDTO> Edit(Auth_AssignDTOUpdate model)
        {
            try
            {
                var auth_Assign = await _QLContext.Auth_AssignRepository.FirstOrDefaultAsync(a => a.ObjectID == model.ObjectID && a.Permission.Contains(model.Permission ?? ""));

                if (auth_Assign == null) throw new AppException("Không tìm thấy dữ liệu phù hợp");

                auth_Assign = _mapper.Map<Auth_Assign>(auth_Assign);

                await _QLContext.Auth_AssignRepository.UpdateAsync(auth_Assign);
                await _QLContext.Auth_AssignRepository.SaveChangesAsync();

                return _mapper.Map<Auth_AssignDTO>(auth_Assign);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }


        #region Tìm kiếm theo ObjectId và Permission
        public async Task<Auth_AssignDTOIndex> Search(Auth_AssignDTOParam searchParam)
        {
            try
            {
                var result = new Auth_AssignDTOIndex();
                var accounts = await (from a in _QLContext.Auth_AssignRepository.GetAll()
                                      where (!string.IsNullOrEmpty(searchParam.Permission) ? a.Permission.Contains(searchParam.Permission) : true)
                                      && (searchParam.ObjectId != null ? a.ObjectID == searchParam.ObjectId : true)
                                      select a).ToListAsync();
                var accountResult = new List<Auth_AssignDTO>();
                foreach (var item in accounts)
                {
                    accountResult.Add(_mapper.Map<Auth_AssignDTO>(item));
                }
                result.Auth_Assigns = accountResult;
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
