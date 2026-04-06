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
    [Table("Auth_Assign")]
    public class Auth_Assign 
    {
        [Key]
        [Required]
        public required string Permission { get; set; }
        [Required]
        public required int ObjectID { get; set; }
    }
}