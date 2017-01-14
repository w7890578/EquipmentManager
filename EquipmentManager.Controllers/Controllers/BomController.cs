using System; 
using System.Web.Mvc;
using JingBaiHui.Common.Models;
using EquipmentManager.Controllers.Models;
using EquipmentManager.Controllers.Provider;

namespace EquipmentManager.Controllers.Controllers
{
    public class BomController : BaseController
    {
       
        [HttpPost]
        public JsonResult Delete(Guid Id)
        {
            BomProvider.Instance.Delete(Id);
            return Json(new ResponseModel() { Status = true });
        }

        public JsonResult Get(Guid Id)
        {
            var model = BomProvider.Instance.Get(Id);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetList(Bom entity)
        {
            var list = BomProvider.Instance.GetEasyUiDataList(entity, this.PageIndex, this.PageSize, this.OrderBy);
            return Json(list, JsonRequestBehavior.AllowGet);
        }

        public ViewResult Index()
        {
            return View();
        }

      
        [HttpPost]
        public JsonResult Save(Bom entity)
        {
            entity.TenantId = this.TenantId;
            if (entity.Id == Guid.Empty)
            {
                entity.Id = Guid.NewGuid();
                entity.CreateBy = this.UserId;
                entity.CreateTime = DateTime.Now;
                entity.ModifyBy = this.UserId;
                entity.ModifyTime = DateTime.Now;
                BomProvider.Instance.Create(entity);
            }
            else
            {
                entity.ModifyBy = this.UserId;
                entity.ModifyTime = DateTime.Now;
               BomProvider.Instance.Update(entity);
            }
            return Json(new ResponseModel() { Status = true });
        }
    }
}