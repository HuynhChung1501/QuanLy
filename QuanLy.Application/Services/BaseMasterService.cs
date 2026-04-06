using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QuanLy.Domain.Interface;

namespace QuanLy.Application.Services
{
    public class BaseMasterService
    {
        protected IQuanLyRepositoryWrapper _travelRepo;
        public BaseMasterService(IQuanLyRepositoryWrapper dasRepository)
        {
            _travelRepo = dasRepository;
        }
    }
}

