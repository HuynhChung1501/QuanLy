using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Domain.Models
{
    [Table("User")]
    public class Auth_Roles 
    {
        [Key]
        public int RoleID { get; set; }
        [Required]
        [MaxLength(250)]
        public required string Name { get; set; }
        [Required]
        [MaxLength(250)]
        public string? Desc { get; set; }
        public int Status { get; set; }
        public byte IsSysAdmin { get; set; }
        public byte IsShow { get; set; }
    }
}
