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
    public class Auth_UsersService : BaseMasterService, IAuth_UsersService
    {
        private readonly IMapper _mapper;
        private readonly IQuanLyRepositoryWrapper _QLContext;
        public Auth_UsersService(IQuanLyRepositoryWrapper QuanLyRepository, IMapper mapper, IQuanLyRepositoryWrapper QlContext) : base(QuanLyRepository)
        {
            _mapper = mapper;
            _QLContext = QuanLyRepository;
        }

        public async Task<Auth_UsersDTODetail> GetID(int id)
        {
            var model = new Auth_UsersDTODetail();
            var auth_Users = await _QLContext.Auth_UsersRepository.FirstOrDefaultAsync(x => x.UserID == id && x.Active == (int)EnumCommon.Status.Active);
            if (auth_Users != null)
            {
                model = _mapper.Map<Auth_UsersDTODetail>(auth_Users);
            }
            else throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            return model;
        }
        public async Task<Auth_UsersDTO> Create(Auth_UsersDTOCreate model)
        {
            try
            {
                if (await _QLContext.Auth_UsersRepository.AnyAsync(a => a.UsereName == model.UsereName))
                {
                    throw new AppException($"UserName: {model.UsereName} đã tồn tại");
                }
                Auth_Users auth_Users = _mapper.Map<Auth_Users>(model);

                await _QLContext.Auth_UsersRepository.InsertAsync(auth_Users);
                await _QLContext.Auth_UsersRepository.SaveChangesAsync();

                return _mapper.Map<Auth_UsersDTO>(model);
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
                var model = await _QLContext.Auth_UsersRepository.FirstOrDefaultAsync(a => a.UserID == id && a.Active == (int)EnumCommon.Status.Active);

                if (model == null) throw new AppException("Không tìm thấy dữ liệu phù hợp.");

                await _QLContext.Auth_UsersRepository.DeleteAsync(model);
                await _QLContext.Auth_UsersRepository.SaveChangesAsync();
                throw new AppException($"Xóa thành công");
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }

        public async Task<Auth_UsersDTO> Edit(Auth_UsersDTOUpdate model)
        {
            try
            {
                var auth_Users = await _QLContext.Auth_UsersRepository.FirstOrDefaultAsync(a => a.UserID == model.UserID);

                if (auth_Users == null) throw new AppException("Không tìm thấy dữ liệu phù hợp");

                auth_Users = _mapper.Map<Auth_Users>(auth_Users);

                await _QLContext.Auth_UsersRepository.UpdateAsync(auth_Users);
                await _QLContext.Auth_UsersRepository.SaveChangesAsync();

                return _mapper.Map<Auth_UsersDTO>(auth_Users);
            }
            catch (Exception ex)
            {
                throw new AppException(ex.Message);
            }
        }


        #region Tìm kiếm theo userName và số điện thoại
        public async Task<Auth_UsersDTOIndex> Search(Auth_UsersDTOParam searchParam)
        {
            try
            {
                var result = new Auth_UsersDTOIndex();
                var accounts = await (from a in _QLContext.Auth_UsersRepository.GetAll()
                                      where (!string.IsNullOrEmpty(searchParam.UsereName) ? a.UsereName.Contains(searchParam.UsereName) : true)
                                      && (string.IsNullOrEmpty(searchParam.Phone) || (a.Phone ?? "").Contains(searchParam.Phone))
                                      && (a.Active == (int)EnumCommon.Status.Active)
                                      select a).ToListAsync();
                var accountResult = new List<Auth_UsersDTO>();
                foreach (var item in accounts)
                {
                    accountResult.Add(_mapper.Map<Auth_UsersDTO>(item));
                }
                result.Positions = accountResult;
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
