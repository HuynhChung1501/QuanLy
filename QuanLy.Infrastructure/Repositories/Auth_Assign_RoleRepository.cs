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
    public class Auth_Assign_RoleRepository : DasBaseRepository<Auth_Assign_role>, IAuth_Assign_RoleRepository
    {
        public Auth_Assign_RoleRepository(DASContext repositoryContext)
           : base(repositoryContext)
        {
        }
    }
}
