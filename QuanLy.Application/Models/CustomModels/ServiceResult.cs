using QuanLy.Application.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuanLy.Application.Models.CustomModels
{
    public class ServiceResult
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public int? TotalRecords { get; set; }
        public object Data { get; set; }

        public ServiceResult() { }
        public ServiceResult(string mesage)
        {
            this.Message = mesage;
        }
    }

    public class ServiceResultSuccess : ServiceResult
    {
        public ServiceResultSuccess()
        {
            Code = CommonConst.Success;
        }
        public ServiceResultSuccess(string msg)
        {
            Code = CommonConst.Success;
            Message = msg;
        }
        public ServiceResultSuccess(object data)
        {
            Code = CommonConst.Success;
            Data = data;
        }
    }

    public class ServiceResultError : ServiceResult
    {
        public ServiceResultError()
        {
            Code = CommonConst.error;
        }
        public ServiceResultError(string msg)
        {
            Code = CommonConst.error;
            Message = msg;
        }
        public ServiceResultError(object data)
        {
            Code = CommonConst.error;
            Data = data;
        }
    }

}
