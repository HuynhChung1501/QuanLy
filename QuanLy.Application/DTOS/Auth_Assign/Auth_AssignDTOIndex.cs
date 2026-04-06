using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_AssignDTOIndex : Auth_AssignDTO
    {
        public List<Auth_AssignDTO> Auth_Assigns { get; set; } = new List<Auth_AssignDTO>();
        public string SortBy { get; set; } = string.Empty;
        public bool SortDesc { get; set; }

    }
}
