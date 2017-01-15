using System;
using System.Text;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using JingBaiHui.Common;
using JingBaiHui.Common.Helper;
using EquipmentManager.Controllers.Models;
using EquipmentManager.Controllers.Constant;

namespace EquipmentManager.Controllers.Dao
{
    public class Re_OrganizationDao : BaseDao
    {
        private static string tableName = "Re_Organization";

        #region singleton

        private static readonly Re_OrganizationDao instance = new Re_OrganizationDao();

        private Re_OrganizationDao()
        {
            if (db == null)
            {
                db = DataBaseFactory.Create("Equipment");
            }
        }

        public static Re_OrganizationDao Instance
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
            var fields = new Dictionary<string, object>()
            {
                { "Id",entity.Id},
                { "TenantId",entity.TenantId},
                { "OrganizationId",entity.OrganizationId},
                { "AssetsId",entity.AssetsId},
                { "EquipmentId",entity.EquipmentId},
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
        public Re_Organization GetById(Guid Id)
        {
            string sql = $@"
                SELECT TOP 1 * FROM [dbo].[{tableName}]
                  WHERE [Id]=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                   {"@Id",Id}
            };
            return DataHelper.GetEntity<Re_Organization>(db, sql, parameters, delegate (IDataReader reader, Re_Organization entity)
            {
                BuildTenant(reader, entity);
            });
        }

        public List<Re_Organization> GetList(
            Guid tenantId,
            List<Guid> organizationIds)
        {
            StringBuilder sql = new StringBuilder();
            sql.Append($@"
                        select * from
                                [dbo].[{tableName}]
                        where   TenantId = @TenantId
                          ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            if (organizationIds != null && organizationIds.Any())
            {
                sql.Append(" and  OrganizationId in (");
                parameters.Add("@TenantId", tenantId);
                for (int i = 0; i < organizationIds.Count; i++)
                {
                    var parameterName = $"@organizationId{i}";
                    parameters.Add(parameterName, organizationIds[i]);
                    if (i == organizationIds.Count - 1)
                    {
                        sql.Append($"{parameterName}");
                    }
                    else
                    {
                        sql.Append($"{parameterName},");
                    }
                }
                sql.Append(")");
            }
            return DataHelper.GetList<Re_Organization>(db, sql.ToString(), parameters, BuildTenant);
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Re_Organization> GetList(Re_Organization entity)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Re_Organization>(db, sql.ToString(), parameters, delegate (IDataReader reader, Re_Organization dataModel)
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
        public List<Re_Organization> GetList(Re_Organization entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Re_Organization>(db, sql.ToString(), parameters, delegate (IDataReader reader, Re_Organization dataModel)
            {
                BuildTenant(reader, dataModel);
            }, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Re_Organization entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "TenantId",entity.TenantId},
                { "OrganizationId",entity.OrganizationId},
                { "AssetsId",entity.AssetsId},
                { "EquipmentId",entity.EquipmentId},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime}
                };
            var filters = new Dictionary<string, object>() {
                { "Id",entity.Id},
                { "TeantId",entity.TenantId}
            };
            DataHelper.Update(db, tableName, fields, filters);
        }

        #region private

        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        private void BuildTenant(IDataReader reader, Re_Organization entity)
        {
            entity.Id = reader.GetValue<Guid>("Id");
            entity.TenantId = reader.GetValue<Guid>("TenantId");
            entity.OrganizationId = reader.GetValue<Guid>("OrganizationId");
            entity.AssetsId = reader.GetValue<Guid>("AssetsId");
            entity.EquipmentId = reader.GetValue<Guid>("EquipmentId");
            entity.CreateBy = reader.GetValue<Guid>("CreateBy");
            entity.CreateTime = reader.GetValue<DateTime>("CreateTime");
            entity.ModifyBy = reader.GetValue<Guid>("ModifyBy");
            entity.ModifyTime = reader.GetValue<DateTime>("ModifyTime");
        }

        /// <summary>
        /// 加载条件
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="parameters">参数</param>
        private void LoadCondition(Re_Organization entity, StringBuilder sql, ref Dictionary<string, object> parameters)
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
            if (entity.OrganizationId != Guid.Empty)
            {
                sql.AppendFormat(" AND [OrganizationId]=@OrganizationId ");
                parameters.Add("@OrganizationId", entity.OrganizationId);
            }
            if (entity.AssetsId != Guid.Empty)
            {
                sql.AppendFormat(" AND [AssetsId]=@AssetsId ");
                parameters.Add("@AssetsId", entity.AssetsId);
            }
            if (entity.EquipmentId != Guid.Empty)
            {
                sql.AppendFormat(" AND [EquipmentId]=@EquipmentId ");
                parameters.Add("@EquipmentId", entity.EquipmentId);
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