using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLy.Domain.Models;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_PermissionsDTOIndex : Auth_PermissionsDTO
    {
        public List<Auth_PermissionsDTO> Auth_Permissions { get; set; } = new List<Auth_PermissionsDTO>();
        public string SortBy { get; set; } = string.Empty;
        public bool SortDesc { get; set; }

    }
}
