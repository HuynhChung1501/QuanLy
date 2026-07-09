using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using QuanLy.Domain.Models;


namespace QuanLy.Infrastructure.Context
{
    public class DASContext : DbContext
    {
        public DASContext(DbContextOptions<DASContext> options) : base(options)
        {

        }
        #region DbSet

        public DbSet<Auth_Users> AuthUsers { get; set; }
        public DbSet<Auth_Assign> Auth_Assigns { get; set; }
        public DbSet<Auth_Assign_role> Auth_Assign_roles { get; set; }
        public DbSet<Auth_Permissions> Auth_Permissions { get; set; }
        public DbSet<Auth_Roles> Auth_Roles { get; set; }
        public DbSet<RefreshToken> RefreshToken { get; set; }
        #endregion
    }
}
