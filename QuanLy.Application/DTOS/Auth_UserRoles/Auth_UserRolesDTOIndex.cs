using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_UserRolesDTOIndex : Auth_UserRolesDTO
    {
        //public PagingResult<Auth_AssignDTO> Positions { get; set; } = new PagingResult<PositionDTO>();
        public string SortBy { get; set; } = string.Empty;
        public bool SortDesc { get; set; }

    }
}
