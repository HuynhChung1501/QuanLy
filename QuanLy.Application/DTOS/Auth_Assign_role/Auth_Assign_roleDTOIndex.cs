using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLy.Domain.Models;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_Assign_roleDTOIndex : Auth_Assign_roleDTO
    {
        public List<Auth_Assign_role> Auth_Assign_roles { get; set; } = new List<Auth_Assign_role>();
        public string SortBy { get; set; } = string.Empty;
        public bool SortDesc { get; set; }

    }
}
