using System;
using System.Web.Mvc;
using JingBaiHui.Common.Models;
using EquipmentManager.Controllers.Models;
using EquipmentManager.Controllers.Provider;

namespace EquipmentManager.Controllers.Controllers
{
    public class UserController : BaseController
    {
        [HttpPost]
        public JsonResult Delete(Guid Id)
        {
            UserProvider.Instance.Delete(Id);
            return Json(new ResponseModel() { Status = true });
        }

        public JsonResult Get(Guid Id)
        {
            var model = UserProvider.Instance.Get(Id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetList(User entity)
        {
            entity.TenantId = this.TenantId;
            var list = UserProvider.Instance.GetEasyUiDataList(entity, this.PageIndex, this.PageSize, this.OrderBy);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(User entity)
        {
            entity.TenantId = this.TenantId;
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                entity.CreateBy = this.UserId;
                entity.CreateTime = DateTime.Now;
                entity.ModifyBy = this.UserId;
                entity.ModifyTime = DateTime.Now;
                UserProvider.Instance.Create(entity);
            }
            else
            {
                entity.ModifyBy = this.UserId;
                entity.ModifyTime = DateTime.Now;
                UserProvider.Instance.Update(entity);
            }
            return Json(new ResponseModel() { Status = true });
        }
    }
}