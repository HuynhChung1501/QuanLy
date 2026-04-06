using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_UsersDTOIndex : Auth_UsersDTO
    {
        public List<Auth_UsersDTO> Positions { get; set; } = new List<Auth_UsersDTO>();
        public string SortBy { get; set; } = string.Empty;
        public bool SortDesc { get; set; }

    }
}
