using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EquipmentManager.Controllers.Constant;
using EquipmentManager.Controllers.Dao;
using EquipmentManager.Controllers.Models;
using JingBaiHui.Common.Models;
using EquipmentManager.Controllers.UIModels;

namespace EquipmentManager.Controllers.Provider
{
    public class EquipmentProvider
    {
        #region singleton

        private static readonly EquipmentProvider instance = new EquipmentProvider();

        private EquipmentProvider()
        {
        }

        public static EquipmentProvider Instance
        {
            get { return instance; }
        }

        #endregion singleton

        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Equipment entity)
        {
            EquipmentDao.Instance.Create(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            EquipmentDao.Instance.Delete(Id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Equipment Get(Guid Id)
        {
            return EquipmentDao.Instance.GetById(Id);
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Equipment Get(Equipment searchEntity)
        {
            var list = EquipmentDao.Instance.GetList(searchEntity);
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
        public EasyUiDataGrid<Equipment> GetEasyUiDataList(Equipment entity, int pageIndex, int pageSize, string order)
        {
            return new EasyUiDataGrid<Equipment>()
            {
                Total = GetRecordCount(entity),
                Rows = GetList(entity, pageIndex, pageSize, order)
            };
        }

        public List<EasyUI_TreeGrid> GetEquipmentTree(Guid tenantId)
        {
            Guid parentId = Guid.Empty;
            List<Guid> equipmentIds = new List<Guid>();

            //取该租户所有位置，
            var organs = OrganizationProvider.Instance.GetTreeAll(tenantId, parentId);
            var organizationIds = organs.Select(t => t.Id).ToList();
            //取所有位置下的 位置与设备关系
            var re_equipments = Re_Organization_EquipmentProvider.Instance.GetList(tenantId, organizationIds);
            //提取所有相关设备Id
            foreach (var orgId in organizationIds)
            {
                equipmentIds.AddRange
                    (
                        GetEquipmentIdsByOrganizationId(tenantId, orgId, re_equipments)
                    );
            }
            //取出所有相关设备Id信息
            var equipments = EquipmentDao.Instance.GetList(tenantId, equipmentIds);

            return OrganizationProvider.Instance.GetTree(tenantId, parentId,
                delegate (EasyUI_TreeGrid treeRow, Organization org)
                {
                    SetEquitpment(treeRow, org, re_equipments, equipments);
                });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Equipment> GetList(Equipment entity)
        {
            return EquipmentDao.Instance.GetList(entity);
        }

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示条数</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<Equipment> GetList(Equipment entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            return EquipmentDao.Instance.GetList(entity, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetRecordCount(Equipment searchEntity)
        {
            var list = GetList(searchEntity);
            return list == null ? 0 : list.Count;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Equipment entity)
        {
            EquipmentDao.Instance.Update(entity);
        }

        private List<Guid> GetEquipmentIdsByOrganizationId(Guid tenantId, Guid organizationId, List<Re_Organization_Equipment> res)
        {
            return res.Where(t => t.TenantId == tenantId && t.OrganizationId == organizationId)
                  .Select(t => t.EquipmentId).ToList();
        }

        private void SetEquitpment(
            EasyUI_TreeGrid treeRow,
            Organization org,
            List<Re_Organization_Equipment> reList,
            List<Equipment> equipmentList
            )
        {
            var res = reList.Where(t => t.OrganizationId.Equals(org.Id)).ToList();
            if (res.Count <= 0)
            {
                return;
            }
            treeRow.Children = treeRow.Children ?? new List<UIModels.EasyUI_TreeGrid>();
            foreach (var re in res)
            {
                var equitpmentInfo = equipmentList.FirstOrDefault(t => t.Id.Equals(re.EquipmentId));
                if (equitpmentInfo != null)
                {
                    var treeChildrenInfo = new EasyUI_TreeGrid()
                    {
                        Id = equitpmentInfo.Id,
                        Name = equitpmentInfo.Name,
                        Type = 2
                    };
                    treeRow.Children.Add(treeChildrenInfo);
                }
            }
        }
    }
}