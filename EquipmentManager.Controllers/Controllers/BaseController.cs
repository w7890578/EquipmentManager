using JingBaiHui.Common;
using JingBaiHui.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace EquipmentManager.Controllers.Controllers
{
    public class BaseController : Controller
    {
        protected virtual string Order
        {
            get
            {
                return Request["order"] ?? "Desc";
            }
        }

        protected virtual string OrderBy
        {
            get
            {
                return string.Concat(" ", SortName, " ", Order, " ");
            }
        }

        protected virtual int PageIndex
        {
            get
            {
                int pageIndex = 1;
                int.TryParse(Request["page"] ?? "1", out pageIndex);
                return pageIndex;
            }
        }

        protected virtual int PageSize
        {
            get
            {
                int pageSize = 15;
                int.TryParse(Request["rows"] ?? "15", out pageSize);
                return pageSize;
            }
        }

        protected virtual string SortName
        {
            get
            {
                return Request["sort"] ?? "CreateTime";
            }
        }

        /// <summary>
        /// 从登录用户的Cookie中取出
        /// </summary>
        protected virtual Guid TenantId
        {
            get
            {
                return Guid.Empty;
            }
        }

        /// <summary>
        /// 从登录用户的Cookie中取出
        /// </summary>
        protected virtual Guid UserId
        {
            get
            {
                return new Guid("A72CFFDE-0FBD-4342-A8A4-BFCF0FBEB779");
            }
        }

        protected override JsonResult Json(object data, string contentType, Encoding contentEncoding, JsonRequestBehavior behavior)
        {
            return new JsonNetResult(data, behavior, contentType, contentEncoding);
        }

        /// <summary>
        /// 通用异常处理信息，如果发生异常返回JSON对象
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            ResponseModel responseModel = new ResponseModel()
            {
                Status = false,
                Msg = filterContext.Exception.Message
            };

            LogHelper.Error(filterContext.Exception);

            filterContext.Result = Json(responseModel, JsonRequestBehavior.AllowGet);
            filterContext.ExceptionHandled = true;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        /// <remarks>仅限单张图片上传</remarks>
        protected string UploadImg()
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase hpf = Request.Files[0] as HttpPostedFileBase;
                filePath = GetUploadFilePath(hpf);
            }
            return filePath;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <returns></returns>
        /// <remarks>仅限单张图片上传</remarks>
        protected string UploadImg(string inputName)
        {
            string filePath = string.Empty;
            if (Request.Files.Count > 0 && Request.Files[inputName] != null)
            {
                HttpPostedFileBase hpf = Request.Files[inputName] as HttpPostedFileBase;
                filePath = GetUploadFilePath(hpf);
            }
            return filePath;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="hpf"></param>
        /// <param name="maxFileSize">限制文件上传大小</param>
        /// <returns></returns>
        private string GetUploadFilePath(HttpPostedFileBase hpf, int maxFileSize = 1024)
        {
            string filePath = string.Empty;
            if (hpf.ContentLength > 0)
            {
                if (hpf.ContentLength / 1024 > maxFileSize)
                {
                    throw new Exception($"上传文件超出限制大小{maxFileSize}kb");
                }
                string savepath = "/Content/images/equipment";
                string fileName = DateTime.Now.ToString("yyyyMMddHHmmssffff") + System.IO.Path.GetExtension(hpf.FileName);
                hpf.SaveAs(string.Concat(Server.MapPath(savepath), @"\", fileName));

                filePath = string.Concat(savepath, "/" + fileName);
            }
            return filePath;
        }
    }
}