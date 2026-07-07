using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using QuanLy.Application.DTO.Auth_Assign;
using QuanLy.Application.Helpers;
using QuanLy.Application.InterfaceService;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;
using QuanLy.Infrastructure.Migrations;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.Services
{
    public class LoginService : BaseMasterService, ILoginService
    {

        private readonly IMapper _mapper;
        private readonly IConfiguration _config;
        private readonly ITokenService _tokenService;
        private readonly IQuanLyRepositoryWrapper _quanLyRepo;


        public LoginService(IQuanLyRepositoryWrapper quanLyRepo, IMapper mapper, ITokenService tokenService, IQuanLyRepositoryWrapper IQuanLyRepositoryWrapper, IConfiguration configuration) : base(quanLyRepo)
        {
            _mapper = mapper;
            _config = configuration;
            _tokenService = tokenService;
            _quanLyRepo = IQuanLyRepositoryWrapper;

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
        public string HashRefreshToken(string refreshToken)
        {
            using (var sha = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(refreshToken);

                var hash = sha.ComputeHash(bytes);

                return Convert.ToBase64String(hash);
            }
        }
        public async Task<ApiResponse> RefreshToken(RefreshTokenRequest RefreshToken)
        {
            ApiResponse apiResponse = new ApiResponse();
            try
            {
                var hashToken = HashRefreshToken(RefreshToken.RefreshToken);

                var resfreshToken = _tokenService.GetRefreshTokenByToken(hashToken) ?? throw new AppException("Resfresh Token không thành công.");
                
                var user = await _quanLyRepo.AuthUser.SingleOrDefaultAsync(u => u.UserID == resfreshToken!.UserId);

                var token = GenerateTokens(user);

                if (resfreshToken!.ExpireAt <= DateTime.UtcNow)
                {
                    throw new AppException("RefreshToken đã hết hạn.");
                }
                if (resfreshToken.RevokedAt != null)
                {
                    throw new AppException("RefreshToken đã bị thu hồi.");
                }

                token.RefreshToken = HashRefreshToken(token.RefreshToken);
                await _tokenService.RevokedToken(resfreshToken);
                bool isInserted = await _tokenService.InsertToken(resfreshToken!.UserId, token.RefreshToken);

                if (!isInserted)
                {
                    throw new AppException("Refresh Token không thành công do không lưu được Token.");
                }

                apiResponse.Message = "Resfresh thành công!";
                apiResponse.Data = token;
                return apiResponse;
            }
            catch (Exception ex)
            {
                throw new AppException($"Có lỗi xảy ra! - {ex.InnerException?.Message}");
            }
        }
    }
}
