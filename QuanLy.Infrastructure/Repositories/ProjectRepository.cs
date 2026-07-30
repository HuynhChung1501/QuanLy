using QuanLy.Domain.Interface;
using QuanLy.Domain.Models;
using QuanLy.Infrastructure.Context;

namespace QuanLy.Infrastructure.Repositories
{
    public class ProjectRepository : DasBaseRepository<Project>, IProjectRepository
    {
        public ProjectRepository(DASContext context) : base(context) { }
    }
}
