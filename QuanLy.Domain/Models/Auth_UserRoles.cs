using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Domain.Models
{
    [Table("Auth_UserRoles")]
    public class Auth_UserRoles
    {
        [Key]
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
}
