using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.DTO.Auth_Assign
{
    public class Auth_AssignDTO : BaseModel
    {
        public string? Permission { get; set; } 
        public int ObjectID { get; set; }
        /// <summary>
        /// Type 1 = , 2 = Role
        /// </summary>
        public int ObjectType { get; set; } = 0;
    }
}
