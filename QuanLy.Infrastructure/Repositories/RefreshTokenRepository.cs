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
    public class RefreshTokenRepository : DasBaseRepository<RefreshToken>, IRefreshTokenRepository
    {
        public RefreshTokenRepository(DASContext repositoryContext)
           : base(repositoryContext)
        {
        }
    }
}
