using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;

namespace QuanLy.Infrastructure.Repositories
{
    public class ProjectTaskRepository : DasBaseRepository<ProjectTask>, IProjectTaskRepository
    {
        public ProjectTaskRepository(DASContext context) : base(context) { }
    }
}
