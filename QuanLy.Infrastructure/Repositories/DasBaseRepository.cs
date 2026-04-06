using QuanLy.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Infrastructure.Repositories
{
    public class DasBaseRepository<T> : BaseRepository<T> where T : class
    {
        protected DASContext DasContext { get; set; }

        public DasBaseRepository(DASContext repositoryContext) : base(repositoryContext)
        {
            DasContext = (DASContext)base.Context;
        }
    }
}
