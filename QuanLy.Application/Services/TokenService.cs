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

        public async Task<bool> InsertToken(int userId, string token)
        {
            try
            {
                var refreshToken = new RefreshToken
                {
                    UserId = userId,
                    TokenHash = token,
                    ExpireAt = DateTime.UtcNow.AddDays(30),
                    CreatedAt = DateTime.UtcNow,
                };

                await _QLContext.RefreshTokenRepository.InsertAsync(refreshToken);
                await _QLContext.RefreshTokenRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error inserting refresh token {ex.InnerException?.Message}");
                return false;
            }
        }
        public async Task<bool> RevokedToken (RefreshToken refreshToken)
        {
            try
            {
                refreshToken.ExpireAt = DateTime.UtcNow;
                await _QLContext.RefreshTokenRepository.UpdateAsync(refreshToken);
                await _QLContext.RefreshTokenRepository.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error RevokedToken {ex.InnerException?.Message}");
                return false;
            }
        }

        public RefreshToken? GetRefreshTokenByToken(string hashToken)
        {
            try
            {
                var token = _QLContext.RefreshTokenRepository.FirstOrDefault(t => t.TokenHash == hashToken);
                return token;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error checking refresh token existence");
                return null;
            }
        }
    }
}
