using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_PermissionsDTO : BaseModel
    {
        public string? Permission { get; set; }
        public string? Name { get; set; }
        public int Status { get; set; }
    }
}
