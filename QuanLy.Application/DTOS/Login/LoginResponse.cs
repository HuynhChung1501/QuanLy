using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class LoginResponse
    {
        [Required]
        public required string AccessToken { get; set; }
        [Required]
        public required string RefreshToken { get; set; }
    }
    public class RefreshTokenRequest
    {
        [Required]
        public required string RefreshToken { get; set; }
    }
}
