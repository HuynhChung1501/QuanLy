using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLy.Domain.Models;

namespace QuanLy.Domain.Models
{
    [Table("Auth_Users")]
    public class Auth_Users : BaseModel
    {
        [Required]
        [MaxLength(250)]
        public required string Name { get; set; } = string.Empty;
        [Required]
        [MaxLength(250)]
        public required string UsereName { get; set; } = string.Empty;
        [Required]
        [MaxLength(250)]
        public required string FullName { get; set; }   = string.Empty ;
        [Required]
        [MaxLength(250)]
        public required string PassWord { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
        public byte IsShow { get; set; }
        public byte Gender { get; set; }
        public byte Active { get; set; }
    }
}
