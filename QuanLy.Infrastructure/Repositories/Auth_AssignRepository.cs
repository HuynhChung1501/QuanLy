using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;

namespace QuanLy.Infrastructure.Repositories
{
    public class Auth_AssignRepository : DasBaseRepository<Auth_Assign>, IAuth_AssignRepository
    {
        public Auth_AssignRepository(DASContext repositoryContext)
           : base(repositoryContext)
        {
        }
    }
}
