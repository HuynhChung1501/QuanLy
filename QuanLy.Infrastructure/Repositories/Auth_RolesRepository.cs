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
    public class Auth_RolesRepository : DasBaseRepository<Auth_Roles>, IAuth_RolesRepository
    {
        public Auth_RolesRepository(DASContext repositoryContext)
           : base(repositoryContext)
        {
        }
    }
}
