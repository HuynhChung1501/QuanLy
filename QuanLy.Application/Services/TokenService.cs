using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using QuanLy.Application.DTO;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Enums;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using Serilog.Core;
namespace QuanLy.Application.Services
{
    public class TokenService : BaseMasterService, ITokenService
    {
        private readonly IMapper _mapper;
        private readonly IQuanLyRepositoryWrapper _QLContext;
        private readonly ILogger<TokenService> _logger;

        public TokenService(IQuanLyRepositoryWrapper QuanLyRepository, IMapper mapper, ILogger<TokenService> logger, IQuanLyRepositoryWrapper QlContext) : base(QuanLyRepository)
        {
            _mapper = mapper;
            _QLContext = QuanLyRepository;
            _logger = logger;
        }

        public bool InsertToken(int id, string token)
        {
            try
            {
                var refreshToken = new RefreshToken
                {
                    UserId = id,
                    TokenHash = token,
                    ExpireAt = DateTime.UtcNow.AddDays(30),
                    CreatedAt = DateTime.Now,
                };

                _QLContext.RefreshTokenRepository.InsertAsync(refreshToken);
                _QLContext.RefreshTokenRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error inserting refresh token");
                return false;
            }
        }
    }
}
