using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLy.Domain.Interface;
using QuanLy.Infrastructure.Repositories;

namespace QuanLy.Application.Services
{
    public class BaseMasterService
    {
        protected IQuanLyRepositoryWrapper _QuanLyRepo;
        public BaseMasterService(IQuanLyRepositoryWrapper dasRepository)
        {
            _QuanLyRepo = dasRepository;
        }
    }
}
