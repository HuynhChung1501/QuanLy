using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using QuanLy.Application.DTO;
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
            var auth_Users = await _QLContext.Auth_UsersRepository.FirstOrDefaultNoTrackingAsync(x => x.UserID == id && x.Active == (int)EnumCommon.Status.Active);
            if (auth_Users == null)
            {
                throw new AppException("Không tìm thấy dữ liệu phù hợp.");
            }
            return _mapper.Map<Auth_UsersDTODetail>(auth_Users);
        }
        public async Task<Auth_UsersDTO> Create(Auth_UsersDTOCreate model)
        {
            try
            {
                model.UsereName = model.UsereName?.Trim().ToLower() ?? string.Empty;

                if (await _QLContext.Auth_UsersRepository
                    .AnyAsync(a => a.UsereName.ToLower() == model.UsereName))
                {
                    throw new AppException($"UserName: {model.UsereName} đã tồn tại");
                }

                var auth_Users = _mapper.Map<Auth_Users>(model);

                auth_Users.Active = (int)EnumCommon.Status.Active;
                auth_Users.CreatedDate = DateTime.UtcNow;

                await _QLContext.Auth_UsersRepository.InsertAsync(auth_Users);
                await _QLContext.Auth_UsersRepository.SaveChangesAsync();

                return _mapper.Map<Auth_UsersDTO>(auth_Users);
            }
            catch (Exception ex)
            {

                throw new AppException($"Có lỗi xảy ra khi tạo mới use", ex.Message);
            }
        }

        public async Task<(bool, string)> Delete(int id)
        {
            try
            {
                var model = await _QLContext.Auth_UsersRepository
                                    .FirstOrDefaultAsync(a => a.UserID == id && a.Active == (int)EnumCommon.Status.Active);

                if (model == null)
                    return (false, "Không tìm thấy dữ liệu phù hơp");

                model.Active = (int)EnumCommon.Status.InActive;

                await _QLContext.Auth_UsersRepository.SaveChangesAsync();

                return (false, "Xóa thành công");
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
                var auth_Users = await _QLContext.Auth_UsersRepository.FirstOrDefaultAsync(a => a.UserID == model.UserID && a.Active == (int)EnumCommon.Status.Active);

                if (auth_Users == null) throw new AppException("Không tìm thấy dữ liệu phù hợp");

                _mapper.Map(model, auth_Users);
                await _QLContext.Auth_UsersRepository.SaveChangesAsync();

                return _mapper.Map<Auth_UsersDTO>(auth_Users);
            }
            catch (Exception ex)
            {
                throw new AppException("Có lỗi xả ra khi cập nhật user", ex.Message);
            }
        }

        public async Task<PagedResult<Auth_UsersDTO>> SearchPagination(Auth_UsersDTOParam searchParam)
        {
            var query = _QLContext.Auth_UsersRepository
                .GetAll()
                .AsNoTracking()
                .Where(a => a.Active == (int)EnumCommon.Status.Active);

            if (!string.IsNullOrEmpty(searchParam.UsereName))
            {
                query = query.Where(a => a.UsereName.Contains(searchParam.UsereName));
            } 

            if (!string.IsNullOrEmpty(searchParam.Phone))
            {
                query = query.Where(a => (a.Phone ?? "").Contains(searchParam.Phone));
            }

            return await query
                .ProjectTo<Auth_UsersDTO>(_mapper.ConfigurationProvider)
                .ToPagedResultAsync(searchParam.PageIndex, searchParam.PageSize);
        }

        #region Tìm kiếm theo userName và số điện thoại
        public async Task<List<Auth_UsersDTO>> Search(Auth_UsersDTOParam searchParam)
        {
            var query = _QLContext.Auth_UsersRepository
                .GetAll()
                .AsNoTracking()
                .Where(a => a.Active == (int)EnumCommon.Status.Active);

            if (!string.IsNullOrEmpty(searchParam.UsereName))
            {
                query = query.Where(a => a.UsereName.ToLower().Contains(searchParam.UsereName.ToLower()));
            }

            if (!string.IsNullOrEmpty(searchParam.Phone))
            {
                query = query.Where(a => (a.Phone ?? "").Contains(searchParam.Phone));
            }

            var data = await query
                .ProjectTo<Auth_UsersDTO>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return data;
        }
        #endregion
    }
}
