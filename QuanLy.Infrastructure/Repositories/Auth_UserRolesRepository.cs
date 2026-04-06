using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;

namespace QuanLy.Infrastructure.Repositories
{
    public class Auth_UserRolesRepository : DasBaseRepository<Auth_UserRoles>, IAuth_UserRolesRepository
    {
        public Auth_UserRolesRepository(DASContext repositoryContext)
           : base(repositoryContext)
        {
        }
    }
}
