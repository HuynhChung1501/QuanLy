using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace QuanLy.Domain.Models
{
    [Table("Auth_Users")]
    public class Auth_Users 
    {
        [Key]
        [Required]
        [Description("Khóa chính")]
        public int UserID { get; set; }
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
        
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? UpdatedBy { get; set; }
    }
}
