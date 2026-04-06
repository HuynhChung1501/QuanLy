using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_RolesDTOIndex : Auth_RolesDTO
    {
        public List<Auth_RolesDTO> AuthRoles { get; set; } = new List<Auth_RolesDTO>();
        public string SortBy { get; set; } = string.Empty;
        public bool SortDesc { get; set; }

    }
}
