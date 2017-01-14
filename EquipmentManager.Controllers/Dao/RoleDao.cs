using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using JingBaiHui.Common;
using JingBaiHui.Common.Helper;
using EquipmentManager.Controllers.Models;
using EquipmentManager.Controllers.Constant;

namespace EquipmentManager.Controllers.Dao
{
    public class RoleDao : BaseDao
    {
        private static string tableName = "Role";

        #region singleton

        private static readonly RoleDao instance = new RoleDao();

        private RoleDao()
        {
            if (db == null)
            {
                db = DataBaseFactory.Create("Equipment");
            }
        }

        public static RoleDao Instance
        {
            get { return instance; }
        }

        #endregion singleton

        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Role entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "Id",entity.Id},
                { "TenantId",entity.TenantId},
                { "Name",entity.Name},
                { "Description",entity.Description},
                { "Remark",entity.Remark},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime}
            };
            DataHelper.Add(db, tableName, fields);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Id"></param>
        public void Delete(Guid Id)
        {
            Dictionary<string, object> filters = new Dictionary<string, object>() {
                { "Id",Id}
            };
            DataHelper.Delete(db, tableName, filters);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public Role GetById(Guid Id)
        {
            string sql = $@"
                SELECT TOP 1 * FROM [dbo].[{tableName}]
                  WHERE [Id]=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                   {"@Id",Id}
            };

            return DataHelper.GetEntity<Role>(db, sql, parameters, delegate (IDataReader reader, Role entity)
            {
                BuildTenant(reader, entity);
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Role> GetList(Role entity)
        {
            StringBuilder sql = new StringBuilder($@"
                SELECT [Role].*,Tenant.Name as TenantName
                    FROM [dbo].[Role]
                left join Tenant
                on
                    [Role].TenantId =[Tenant].Id");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Role>(db, sql.ToString(), parameters, delegate (IDataReader reader, Role dataModel)
            {
                BuildTenant(reader, dataModel);
            });
        }

        /// <summary>
        /// 分页获取列表
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">一页显示条数</param>
        /// <param name="order">排序</param>
        /// <returns></returns>
        public List<Role> GetList(Role entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            StringBuilder sql = new StringBuilder($@"
                SELECT [Role].*,Tenant.Name as TenantName
                    FROM [dbo].[Role]
                left join Tenant
                on
                    [Role].TenantId =[Tenant].Id ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Role>(db, sql.ToString(), parameters, delegate (IDataReader reader, Role dataModel)
            {
                BuildTenant(reader, dataModel);
            }, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Role entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "TenantId",entity.TenantId},
                { "Name",entity.Name},
                { "Description",entity.Description},
                { "Remark",entity.Remark},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime}
                };
            var filters = new Dictionary<string, object>() {
                { "Id",entity.Id},
                { "TenantId",entity.TenantId}
            };
            DataHelper.Update(db, tableName, fields, filters);
        }

        #region private

        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        private void BuildTenant(IDataReader reader, Role entity)
        {
            entity.Id = reader.GetValue<Guid>("Id");
            entity.TenantId = reader.GetValue<Guid>("TenantId");
            entity.Name = reader.GetValue<string>("Name");
            entity.Description = reader.GetValue<string>("Description");
            entity.Remark = reader.GetValue<string>("Remark");
            entity.CreateBy = reader.GetValue<Guid>("CreateBy");
            entity.CreateTime = reader.GetValue<DateTime>("CreateTime");
            entity.ModifyBy = reader.GetValue<Guid>("ModifyBy");
            entity.ModifyTime = reader.GetValue<DateTime>("ModifyTime");
            entity.TenantName = reader.GetValue<string>("TenantName");
        }

        /// <summary>
        /// 加载条件
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="parameters">参数</param>
        private void LoadCondition(Role entity, StringBuilder sql, ref Dictionary<string, object> parameters)
        {
            if (entity == null)
                return;

            sql.AppendFormat(" WHERE 1=1 ");

            if (entity.Id != Guid.Empty)
            {
                sql.AppendFormat(" AND [Id]=@Id ");
                parameters.Add("@Id", entity.Id);
            }
            if (entity.TenantId != Guid.Empty)
            {
                sql.AppendFormat(" AND [TenantId]=@TenantId ");
                parameters.Add("@TenantId", entity.TenantId);
            }
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                sql.AppendFormat(" AND [Name] LIKE '%'+@Name+'%' ");
                parameters.Add("@Name", entity.Name);
            }

            if (!string.IsNullOrWhiteSpace(entity.Description))
            {
                sql.AppendFormat(" AND [Description] LIKE '%'+@Description+'%' ");
                parameters.Add("@Description", entity.Description);
            }

            if (!string.IsNullOrWhiteSpace(entity.Remark))
            {
                sql.AppendFormat(" AND [Remark] LIKE '%'+@Remark+'%' ");
                parameters.Add("@Remark", entity.Remark);
            }

            if (entity.CreateBy != Guid.Empty)
            {
                sql.AppendFormat(" AND [CreateBy]=@CreateBy ");
                parameters.Add("@CreateBy", entity.CreateBy);
            }

            if (entity.ModifyBy != Guid.Empty)
            {
                sql.AppendFormat(" AND [ModifyBy]=@ModifyBy ");
                parameters.Add("@ModifyBy", entity.ModifyBy);
            }
        }

        #endregion private
    }
}