using Microsoft.AspNetCore.Mvc;
using QuanLy.Application.Constants;
using QuanLy.Application.Models.CustomModels;
using QuanLy.Domain.Models;

namespace QuanLyCongViec.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult CustJSonResult(ServiceResult serviceResult)
        {
            if (serviceResult.Code == CommonConst.Success)
            {
                return JSSuccessResult(serviceResult.Message, serviceResult.Data);
            }
            if (serviceResult.Code == CommonConst.error)
            {
                return JSErrorResult(serviceResult.Message, serviceResult.Data);
            }
            if (serviceResult.Code == CommonConst.warning)
            {
                return JSWarningResult(serviceResult.Message, serviceResult.Data);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Trả về JSon Code Success
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IActionResult JSSuccessResult(string msg)
        {
            return Ok(new JsonData()
            {
                Status = CommonConst.Success,
                Message = msg
            });
        }

        /// <summary>
        /// Trả về JSon Code Success
        /// trả thêm data dạng Hashtable
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IActionResult JSSuccessResult<T>(string msg, T val)
        {
            return Ok(new JsonData()
            {
                Status = CommonConst.Success,
                Message = msg,
                Data = val
            });
        }

        /// <summary>
        /// Trả về JSon Code Error
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IActionResult JSErrorResult(string msg)
        {
            return BadRequest(new JsonData()
            {
                Status = CommonConst.error,
                Message = msg
            });
        }

        /// <summary>
        /// Trả về JSon Code Error,
        /// Trả cả data dạng hashtable
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IActionResult JSErrorResult<T>(string msg, T val)
        {
            var a = new JsonData()
            {
                Status = CommonConst.error,
                Message = msg,
                Data = val
            };
            return Json(a);
        }

        /// <summary>
        /// Trả về JSon Code Warning
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IActionResult JSWarningResult(string msg)
        {
            var a = new JsonData()
            {
                Status = CommonConst.warning,
                Message = msg
            };
            return Json(a);
        }

        /// <summary>
        /// Trả về JSon Code Warning,
        /// Trả cả data dạng hashtable
        /// </summary>
        /// <param name="msg"></param>
        /// <returns></returns>
        protected IActionResult JSWarningResult<T>(string msg, T val)
        {
            var a = new JsonData()
            {
                Status = CommonConst.warning,
                Message = msg,
                Data = val
            };
            return Json(a);
        }

    }
}
