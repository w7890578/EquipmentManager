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
    public class Re_OrganizationProvider
    {
        #region singleton

        private static readonly Re_OrganizationProvider instance = new Re_OrganizationProvider();

        private Re_OrganizationProvider()
        {
        }

        public static Re_OrganizationProvider Instance
        {
            get { return instance; }
        }

        #endregion singleton

        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Re_Organization entity)
        {
            Re_OrganizationDao.Instance.Create(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            Re_OrganizationDao.Instance.Delete(Id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Re_Organization Get(Guid Id)
        {
            return Re_OrganizationDao.Instance.GetById(Id);
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Re_Organization Get(Re_Organization searchEntity)
        {
            var list = Re_OrganizationDao.Instance.GetList(searchEntity);
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
        public EasyUiDataGrid<Re_Organization> GetEasyUiDataList(Re_Organization entity, int pageIndex, int pageSize, string order)
        {
            return new EasyUiDataGrid<Re_Organization>()
            {
                Total = GetRecordCount(entity),
                Rows = GetList(entity, pageIndex, pageSize, order)
            };
        }

        public List<Re_Organization> GetList(Guid tenantId, List<Guid> organizationIds)
        {
            return Re_OrganizationDao.Instance.GetList(tenantId, organizationIds);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Re_Organization> GetList(Re_Organization entity)
        {
            return Re_OrganizationDao.Instance.GetList(entity);
        }

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示条数</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<Re_Organization> GetList(Re_Organization entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            return Re_OrganizationDao.Instance.GetList(entity, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetRecordCount(Re_Organization searchEntity)
        {
            var list = GetList(searchEntity);
            return list == null ? 0 : list.Count;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Re_Organization entity)
        {
            Re_OrganizationDao.Instance.Update(entity);
        }
    }
}