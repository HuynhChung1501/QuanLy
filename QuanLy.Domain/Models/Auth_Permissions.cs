using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Domain.Models
{

    [Table("Auth_Permissions")]
    public class Auth_Permissions 
    {
        [Key]
        [Required]
        public required string Permission { get; set; }
        [Required]
        [MaxLength(250)]
        public required string Name { get; set; }
        public int Status { get; set; }
    }
}