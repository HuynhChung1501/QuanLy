using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Infrastructure.Repositories
{
    public class AuthUsersRepository : DasBaseRepository<Auth_Users>, IAuthUsersRepository
    {
        public AuthUsersRepository(DASContext repositoryContext)
            : base(repositoryContext)
        {
        }
    }
}
