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
    public class UserProvider
    {
        #region singleton

        private static readonly UserProvider instance = new UserProvider();

        private UserProvider()
        {
        }

        public static UserProvider Instance
        {
            get { return instance; }
        }

        #endregion singleton
 
        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(User entity)
        {
            UserDao.Instance.Create(entity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            UserDao.Instance.Delete(Id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public User Get(Guid Id)
        {
            return UserDao.Instance.GetById(Id);
        }

        /// <summary>
        /// 根据条件获取
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public User Get(User searchEntity)
        {
            var list = UserDao.Instance.GetList(searchEntity);
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
        public EasyUiDataGrid<User> GetEasyUiDataList(User entity, int pageIndex, int pageSize, string order)
        {
            return new EasyUiDataGrid<User>()
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
        public List<User> GetList(User entity)
        {
            return UserDao.Instance.GetList(entity);
        }

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示条数</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<User> GetList(User entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            return UserDao.Instance.GetList(entity, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 获取记录总数
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int GetRecordCount(User searchEntity)
        {
            var list = GetList(searchEntity);
            return list == null ? 0 : list.Count;
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(User entity)
        {
            UserDao.Instance.Update(entity);
        }
    }
}