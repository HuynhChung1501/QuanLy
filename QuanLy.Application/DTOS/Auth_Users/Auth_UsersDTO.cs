using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_UsersDTO : BaseModel
    {
        public int UserID { get; set; }
        [Required(ErrorMessage = "Trường Name không được để trống")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Trường UserName không được để trống")]
        public string UsereName { get; set; }
        [Required(ErrorMessage = "Trường full name không được để trống")]
        public string FullName { get; set; }
        [Required(ErrorMessage = "Mật khẩu không được để trống")]
        public string PassWord { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
        public byte IsShow { get; set; } = 1;
        public byte Gender { get; set; } = 1;
        public byte Active { get; set; } = 1;
    }
}
