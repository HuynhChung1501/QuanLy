using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Domain.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Trường UsereName không được để trống")]
        [MaxLength(50, ErrorMessage = "Trường UsereName không được vượt quá 50 ký tự")]
        public required string UsereName { get; set; }

        [Required(ErrorMessage = "Trường PassWord không được để trống")]
        [MaxLength(50)]
        public required string PassWord { get; set; }
    }
}
