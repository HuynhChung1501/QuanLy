using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_UsersDTOParam
    {
        public required string UsereName { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int DepartmentId { get; set; }
        public byte IsShow { get; set; }
        public byte Gender { get; set; }
        public byte Active { get; set; }
    }
}
