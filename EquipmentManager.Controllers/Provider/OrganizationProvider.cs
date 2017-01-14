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

        /// <summary>
        /// 获取组织机构树形结构
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="parentId"></param>
        /// <param name="actions">递归遍历时注入的函数</param>
        /// <returns></returns>
        public List<EasyUI_TreeGrid> GetTree(
            Guid tenantId,
            Guid parentId,
            params Action<EasyUI_TreeGrid, Organization>[] actions
            )
        {
            var list = OrganizationDao.Instance.GetTreeAll(tenantId, parentId);
            return BuildTree(tenantId, parentId, list, actions);
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
    }
}