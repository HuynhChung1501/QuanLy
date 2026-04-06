using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_UserRolesDTO : BaseModel
    {
        public int UserID { get; set; }
        public int RoleID { get; set; }
    }
}
