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
using EquipmentManager.Controllers.Enum;

namespace EquipmentManager.Controllers.Provider
{
    public class OrganizationProvider
    {
        #region singleton

        private static readonly OrganizationProvider instance = new OrganizationProvider();

        private OrganizationProvider()
        {
        }

        public static OrganizationProvider Instance
        {
            get { return instance; }
        }

        #endregion singleton

        public List<EasyUI_TreeGrid> GetTreeGrid(
            Guid tenantId,
            Guid parentId,
            TreeGridTypeEnum treeGridTypeEnum = TreeGridTypeEnum.OrganizationAll)
        {
            List<Guid> equipmentIds = new List<Guid>();
            List<Guid> assetsIds = new List<Guid>();

            //取该租户所有位置，
            var organs = GetTreeAll(tenantId, parentId);
            var organizationIds = organs.Select(t => t.Id).Distinct().ToList();
            organizationIds.RemoveAll(t => t == null);

            //取所有关系
            var reList = Re_OrganizationProvider.Instance.GetList(tenantId, organizationIds);

            if (treeGridTypeEnum.Equals(TreeGridTypeEnum.OrganizationAll))
            {
                var equipments = GetEquipmentList(tenantId, organizationIds, reList);
                var assetss = GetAssetsList(tenantId, organizationIds, reList);
                return GetTreeGrid(
                  tenantId,
                  parentId,
                  delegate (EasyUI_TreeGrid treeRow, Organization org)
                  {
                      SetEquitpment(treeRow, org, reList, equipments);
                  },
                  delegate (EasyUI_TreeGrid treeRow, Organization org)
                  {
                      SetAssets(treeRow, org, reList, assetss);
                  });
            }
            else if (treeGridTypeEnum.Equals(TreeGridTypeEnum.OrganizationAssets))
            {
                var assetss = GetAssetsList(tenantId, organizationIds, reList);
                return GetTreeGrid(
                  tenantId,
                  parentId,
                  delegate (EasyUI_TreeGrid treeRow, Organization org)
                  {
                      SetAssets(treeRow, org, reList, assetss);
                  });
            }
            else if (treeGridTypeEnum.Equals(TreeGridTypeEnum.OrganizationEquipment))
            {
                var equipments = GetEquipmentList(tenantId, organizationIds, reList);
                return GetTreeGrid(
                  tenantId,
                  parentId,
                  delegate (EasyUI_TreeGrid treeRow, Organization org)
                  {
                      SetEquitpment(treeRow, org, reList, equipments);
                  });
            }
            return new List<EasyUI_TreeGrid>();
        }

        private List<Assets> GetAssetsList(
            Guid tenantId,
            List<Guid> organizationIds,
            List<Re_Organization> reList)
        {
            var assetsIds = new List<Guid>();
            foreach (var orgId in organizationIds)
            {
                assetsIds.AddRange
                   (
                       GetAssetsIdsByOrganizationId(tenantId, orgId, reList)
                   );
            }
            assetsIds = assetsIds.Distinct().ToList();
            assetsIds.RemoveAll(t => t == null);
            return AssetsDao.Instance.GetList(tenantId, assetsIds);
        }

        private List<Equipment> GetEquipmentList(
                            Guid tenantId,
            List<Guid> organizationIds,
            List<Re_Organization> reList)
        {
            var equipmentIds = new List<Guid>();
            foreach (var orgId in organizationIds)
            {
                equipmentIds.AddRange
                    (
                        GetEquipmentIdsByOrganizationId(tenantId, orgId, reList)
                    );
            }
            equipmentIds = equipmentIds.Distinct().ToList();
            equipmentIds.RemoveAll(t => t == null);
            return EquipmentDao.Instance.GetList(tenantId, equipmentIds);
        }

        #region Equipment

        private List<Guid> GetEquipmentIdsByOrganizationId(Guid tenantId, Guid organizationId, List<Re_Organization> res)
        {
            var result = res.Where(t => t.TenantId == tenantId && t.OrganizationId == organizationId)
                  .Select(t => t.EquipmentId).ToList();
            result.RemoveAll(t => t == null);
            return result;
        }

        private void SetEquitpment(
            EasyUI_TreeGrid treeRow,
            Organization org,
            List<Re_Organization> reList,
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

        #endregion Equipment

        #region Assets

        private List<Guid> GetAssetsIdsByOrganizationId(Guid tenantId, Guid organizationId, List<Re_Organization> res)
        {
            List<Guid> result = res.Where(t => t.TenantId == tenantId && t.OrganizationId == organizationId)
                  .Select(t => t.AssetsId).ToList();
            result.RemoveAll(t => t == null);
            return result;
        }

        private void SetAssets(
           EasyUI_TreeGrid treeRow,
           Organization org,
           List<Re_Organization> reList,
           List<Assets> assetsList
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
                var assetsInfo = assetsList.FirstOrDefault(t => t.Id.Equals(re.AssetsId));
                if (assetsInfo != null)
                {
                    var treeChildrenInfo = new EasyUI_TreeGrid()
                    {
                        Id = assetsInfo.Id,
                        Name = assetsInfo.Name,
                        Type = 3
                    };
                    treeRow.Children.Add(treeChildrenInfo);
                }
            }
        }

        #endregion Assets

        #region CRUD

        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Organization entity)
        {
            OrganizationDao.Instance.Create(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            OrganizationDao.Instance.Delete(Id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Organization Get(Guid Id)
        {
            return OrganizationDao.Instance.GetById(Id);
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public Organization Get(Organization searchEntity)
        {
            var list = OrganizationDao.Instance.GetList(searchEntity);
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
        public EasyUiDataGrid<Organization> GetEasyUiDataList(Organization entity, int pageIndex, int pageSize, string order)
        {
            return new EasyUiDataGrid<Organization>()
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
        public List<Organization> GetList(Organization entity)
        {
            return OrganizationDao.Instance.GetList(entity);
        }

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示条数</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<Organization> GetList(Organization entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            return OrganizationDao.Instance.GetList(entity, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetRecordCount(Organization searchEntity)
        {
            var list = GetList(searchEntity);
            return list == null ? 0 : list.Count;
        }

        public List<Organization> GetTreeAll(Guid tenantId, Guid parentId)
        {
            return OrganizationDao.Instance.GetTreeAll(tenantId, parentId);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Organization entity)
        {
            OrganizationDao.Instance.Update(entity);
        }

        private List<EasyUI_TreeGrid> BuildTree(
                 Guid tenantId,
                 Guid parentId,
                 List<Organization> orgList,
                 params Action<EasyUI_TreeGrid, Organization>[] actions
                    )
        {
            var result = new List<EasyUI_TreeGrid>();
            var orgs = orgList.Where(t => t.ParentId.Equals(parentId));
            foreach (var org in orgs)
            {
                var treeRow = new EasyUI_TreeGrid()
                {
                    Id = org.Id,
                    Name = org.Name,
                    Type = 1
                };
                result.Add(treeRow);
                SetChildren(treeRow, org, orgList, actions);
            }
            return result;
        }

        /// <summary>
        /// 获取组织机构树形结构
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="parentId"></param>
        /// <param name="actions">递归遍历时注入的函数</param>
        /// <returns></returns>
        private List<EasyUI_TreeGrid> GetTreeGrid(
            Guid tenantId,
            Guid parentId,
            params Action<EasyUI_TreeGrid, Organization>[] actions
            )
        {
            var list = OrganizationDao.Instance.GetTreeAll(tenantId, parentId);
            return BuildTree(tenantId, parentId, list, actions);
        }

        private void SetChildren(
           EasyUI_TreeGrid treeRow,
           Organization org,
           List<Organization> orgList,
           params Action<EasyUI_TreeGrid, Organization>[] actions
           )
        {
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    action(treeRow, org);
                }
            }
            var orgChildrens = orgList.Where(t => t.ParentId.Equals(org.Id)).ToList();
            if (orgChildrens.Count <= 0)
            {
                return;
            }
            treeRow.Children = treeRow.Children ?? new List<UIModels.EasyUI_TreeGrid>();
            foreach (var entity in orgChildrens)
            {
                var treeChildrenInfo = new EasyUI_TreeGrid()
                {
                    Id = entity.Id,
                    Name = entity.Name,
                    Type = 1
                };
                treeRow.Children.Add(treeChildrenInfo);
                SetChildren(treeChildrenInfo, entity, orgList);
            }
        }

        #endregion CRUD
    }
}