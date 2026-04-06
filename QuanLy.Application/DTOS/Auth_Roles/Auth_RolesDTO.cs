using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_RolesDTO : BaseModel
    {
        public int RoleID { get; set; }
        public string? Name { get; set; }
        public string? Desc { get; set; }
        public int Status { get; set; }
        public byte IsSysAdmin { get; set; }
        public byte IsShow { get; set; }
    }
}
