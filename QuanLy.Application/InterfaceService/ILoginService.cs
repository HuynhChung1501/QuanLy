using QuanLy.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.InterfaceService
{
    public interface ILoginService
    {
        string GenerateToken(Auth_Users acount);
    }
}
