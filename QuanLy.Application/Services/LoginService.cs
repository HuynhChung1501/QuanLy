using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace QuanLy.Application.Services
{
    public class LoginService : BaseMasterService, ILoginService
    {

        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IQuanLyRepositoryWrapper _quanLyRepo;
        private readonly ILogger<LoginService> _logger;
        private readonly DASContext _context;


        public LoginService(IQuanLyRepositoryWrapper quanLyRepo, IMapper mapper, ITokenService tokenService, ILogger<LoginService> logger, IQuanLyRepositoryWrapper IQuanLyRepositoryWrapper, IConfiguration configuration, DASContext context) : base(quanLyRepo)
        {
            _mapper = mapper;
            _config = configuration;
            _tokenService = tokenService;
            _quanLyRepo = IQuanLyRepositoryWrapper;
            _logger = logger;
            _context = context;


        }
        public LoginResponse GenerateTokens(Auth_Users acount)
        {
            var JwtTokenHadler = new JwtSecurityTokenHandler();
            var secretKey = _config["AppSettings:SecretKey"];
            var secretKeyBytes = Encoding.UTF8.GetBytes(secretKey ?? string.Empty);

            var TokenDescription = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] {
                    new Claim(ClaimTypes.Name, acount.FullName),
                    new Claim(ClaimTypes.Email, acount.Email ?? string.Empty),
                    new Claim("UsereName", acount.UsereName),
                    new Claim("UserID", acount.UserID.ToString()),  

                    //Roles

                    new Claim("TokenId", Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyBytes), SecurityAlgorithms.HmacSha256Signature)
            };

            var toKen = JwtTokenHadler.CreateToken(TokenDescription ?? new SecurityTokenDescriptor());
            return new LoginResponse
            {
                AccessToken = JwtTokenHadler.WriteToken(toKen),
                RefreshToken = GenerateRefreshToken(),
            };
        }

        public string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
            }

            return Convert.ToBase64String(randomNumber);
        }
        public string HashToken(string refreshToken)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(refreshToken);

                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
        public async Task<ApiResponse> RefreshAccessToken(RefreshTokenRequest request)
        {
            ArgumentNullException.ThrowIfNull(request);

            if (string.IsNullOrWhiteSpace(request.RefreshToken))
            {
                throw new AppException("Refresh token không được để trống.");
            }

            var hashedRefreshToken = HashToken(request.RefreshToken);

            var refreshToken = await _tokenService.GetRefreshTokenByToken(hashedRefreshToken)
                ?? throw new AppException("Refresh token không tồn tại.");

            if (refreshToken.ExpireAt <= DateTime.UtcNow)
            {
                throw new AppException("Refresh token đã hết hạn.");
            }

            if (refreshToken.RevokedAt != null)
            {
                throw new AppException("Refresh token đã bị thu hồi.");
            }

            var user = await _quanLyRepo.AuthUser
                .SingleOrDefaultAsync(x => x.UserID == refreshToken.UserId)
                ?? throw new AppException("User không tồn tại.");

            var newToken = GenerateTokens(user);

            // TODO: Thực hiện revoke + insert trong cùng transaction
            await RotateRefreshTokenAsync(refreshToken, newToken.RefreshToken);

            await _tokenService.RevokedToken(refreshToken);

            if (!await _tokenService.InsertToken(refreshToken.UserId, HashToken(newToken.RefreshToken)))
            {
                throw new AppException("Không thể lưu refresh token mới.");
            }

            return new ApiResponse
            {
                Success = true,
                Message = "Refresh token thành công.",
                Data = newToken
            };
        }
        public async Task RotateRefreshTokenAsync(RefreshToken refreshToken, string newRefreshToken)
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            try { 
                refreshToken.RevokedAt = DateTime.UtcNow; 
                _context.RefreshToken.Update(refreshToken); 
                _context.RefreshToken.Add(new RefreshToken { 
                    UserId = refreshToken.UserId, 
                    TokenHash = HashToken(newRefreshToken), 
                    ExpireAt = DateTime.UtcNow.AddDays(7) 
                }); 
                await _context.SaveChangesAsync(); 
                await transaction.CommitAsync(); 
            } 
            catch (Exception ex) { 
                await transaction.RollbackAsync();
                _logger.LogError(ex, $"Cõ lỗi xảy ra: {ex.Message}");
                throw; 
            }
        }


    }
}
