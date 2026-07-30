using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;

namespace QuanLy.Infrastructure.Repositories
{
    public class SprintRepository : DasBaseRepository<Sprint>, ISprintRepository
    {
        public SprintRepository(DASContext context) : base(context) { }
    }
}
