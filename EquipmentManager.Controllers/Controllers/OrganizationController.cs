using System;
using System.Web.Mvc;
using JingBaiHui.Common.Models;
using EquipmentManager.Controllers.Models;
using EquipmentManager.Controllers.Provider;
using EquipmentManager.Controllers.Enum;

namespace EquipmentManager.Controllers.Controllers
{
    public class OrganizationController : BaseController
    {
        [HttpPost]
        public JsonResult Delete(Guid Id)
        {
            OrganizationProvider.Instance.Delete(Id);
            return Json(new ResponseModel() { Status = true });
        }

        public ViewResult Detail()
        {
            return View();
        }

        public JsonResult Get(Guid Id)
        {
            var model = OrganizationProvider.Instance.Get(Id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetList(Organization entity)
        {
            entity.TenantId = this.TenantId;
            var list = OrganizationProvider.Instance.GetEasyUiDataList(entity, this.PageIndex, this.PageSize, this.OrderBy);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetTree(TreeGridTypeEnum treeGridTypeEnum)
        {
            var list = OrganizationProvider.Instance.GetTreeGrid(this.TenantId, Guid.Empty, treeGridTypeEnum);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Save(Organization entity)
        {
            entity.TenantId = this.TenantId;
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                entity.CreateBy = this.UserId;
                entity.CreateTime = DateTime.Now;
                entity.ModifyBy = this.UserId;
                entity.ModifyTime = DateTime.Now;
                OrganizationProvider.Instance.Create(entity);
            }
            else
            {
                entity.ModifyBy = this.UserId;
                entity.ModifyTime = DateTime.Now;
                OrganizationProvider.Instance.Update(entity);
            }
            return Json(new ResponseModel() { Status = true });
        }
    }
}