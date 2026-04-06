using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_RolesDTOParam
    {
        public string? Permission { get; set; }
        public string? Name { get; set; }
        public byte IsShow { get; set; }
    }
}
