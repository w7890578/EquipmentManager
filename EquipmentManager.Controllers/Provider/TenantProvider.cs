using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManager.Controllers.Constant;
using EquipmentManager.Controllers.Dao;
using EquipmentManager.Controllers.Models;  
using JingBaiHui.Common.Models;

namespace EquipmentManager.Controllers.Provider
{
    public class TenantProvider
    {
        #region singleton

        private static readonly TenantProvider instance = new TenantProvider();

        private TenantProvider()
        {
        }

        public static TenantProvider Instance
        {
            get { return instance; }
        }

        #endregion singleton
 
        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Tenant entity)
        {
            TenantDao.Instance.Create(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            TenantDao.Instance.Delete(Id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Tenant Get(Guid Id)
        {
            return TenantDao.Instance.GetById(Id);
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Tenant Get(Tenant searchEntity)
        {
            var list = TenantDao.Instance.GetList(searchEntity);
            if (list != null && list.Count > 0)
            {
                return list[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获取EasyUiDataGrid数据集
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EasyUiDataGrid<Tenant> GetEasyUiDataList(Tenant entity, int pageIndex, int pageSize, string order)
        {
            return new EasyUiDataGrid<Tenant>()
            {
                Total = GetRecordCount(entity),
                Rows = GetList(entity, pageIndex, pageSize, order)
            };
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Tenant> GetList(Tenant entity)
        {
            return TenantDao.Instance.GetList(entity);
        }

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示条数</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<Tenant> GetList(Tenant entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            return TenantDao.Instance.GetList(entity, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetRecordCount(Tenant searchEntity)
        {
            var list = GetList(searchEntity);
            return list == null ? 0 : list.Count;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Tenant entity)
        {
            TenantDao.Instance.Update(entity);
        }
    }
}