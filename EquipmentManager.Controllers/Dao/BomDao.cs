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
    public class BomDao : BaseDao
    {
        private static string tableName = "Bom";

        #region singleton

        private static readonly BomDao instance = new BomDao();

        private BomDao()
        {
            if (db == null)
            {
                db = DataBaseFactory.Create(EquipmentConst.DataBaseName);
            }
        }

        public static BomDao Instance
        {
            get { return instance; }
        }

        #endregion singleton

        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Bom entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "Id",entity.Id},
                { "TenantId",entity.TenantId},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime},
                { "Model",entity.Model},
                { "Name",entity.Name},
                { "UnitSingleUnit",entity.UnitSingleUnit},
                { "EquipmentId",entity.EquipmentId},
                { "ComponentId",entity.ComponentId}
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
        public Bom GetById(Guid Id)
        {
            string sql = $@"
                SELECT TOP 1 * FROM [dbo].[{tableName}]
                  WHERE [Id]=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                   {"@Id",Id}
            };
            return DataHelper.GetEntity<Bom>(db, sql, parameters, delegate (IDataReader reader, Bom entity)
            {
                BuildTenant(reader, entity);
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Bom> GetList(Bom entity)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Bom>(db, sql.ToString(), parameters, delegate (IDataReader reader, Bom dataModel)
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
        public List<Bom> GetList(Bom entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Bom>(db, sql.ToString(), parameters, delegate (IDataReader reader, Bom dataModel)
            {
                BuildTenant(reader, dataModel);
            }, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Bom entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "TenantId",entity.TenantId},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime},
                { "Model",entity.Model},
                { "Name",entity.Name},
                { "UnitSingleUnit",entity.UnitSingleUnit},
                { "EquipmentId",entity.EquipmentId},
                { "ComponentId",entity.ComponentId}
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
        private void BuildTenant(IDataReader reader, Bom entity)
        {
            entity.Id = reader.GetValue<Guid>("Id");
            entity.TenantId = reader.GetValue<Guid>("TenantId");
            entity.CreateBy = reader.GetValue<Guid>("CreateBy");
            entity.CreateTime = reader.GetValue<DateTime>("CreateTime");
            entity.ModifyBy = reader.GetValue<Guid>("ModifyBy");
            entity.ModifyTime = reader.GetValue<DateTime>("ModifyTime");
            entity.Model = reader.GetValue<string>("Model");
            entity.Name = reader.GetValue<string>("Name");
            entity.UnitSingleUnit = reader.GetValue<decimal>("UnitSingleUnit");
            entity.EquipmentId = reader.GetValue<Guid>("EquipmentId");
            entity.ComponentId = reader.GetValue<Guid>("ComponentId");
        }

        /// <summary>
        /// 加载条件
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="parameters">参数</param>
        private void LoadCondition(Bom entity, StringBuilder sql, ref Dictionary<string, object> parameters)
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

            if (!string.IsNullOrWhiteSpace(entity.Model))
            {
                sql.AppendFormat(" AND [Model]=@Model ");
                parameters.Add("@Model", entity.Model);
            }

            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                sql.AppendFormat(" AND [Name]=@Name ");
                parameters.Add("@Name", entity.Name);
            }

            if (entity.EquipmentId != Guid.Empty)
            {
                sql.AppendFormat(" AND [EquipmentId]=@EquipmentId ");
                parameters.Add("@EquipmentId", entity.EquipmentId);
            }
            if (entity.ComponentId != Guid.Empty)
            {
                sql.AppendFormat(" AND [ComponentId]=@ComponentId ");
                parameters.Add("@ComponentId", entity.ComponentId);
            }
        }

        #endregion private
    }
}