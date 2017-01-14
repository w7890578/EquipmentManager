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
    public class TenantDao : BaseDao
    {
        private static string tableName = "Tenant";

        #region singleton

        private static readonly TenantDao instance = new TenantDao();

        private TenantDao()
        {
            if (db == null)
            {
                db = DataBaseFactory.Create("Equipment");
            }
        }

        public static TenantDao Instance
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
            var fields = new Dictionary<string, object>()
            {
                { "Id",entity.Id},
                { "Name",entity.Name},
                { "ContactUser",entity.ContactUser},
                { "ContactPhone",entity.ContactPhone},
                { "Address",entity.Address},
                { "Description",entity.Description},
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
        public Tenant GetById(Guid Id)
        {
            string sql = $@"
                SELECT TOP 1 * FROM [dbo].[{tableName}]
                  WHERE [Id]=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                   {"@Id",Id}
            };
            return DataHelper.GetEntity<Tenant>(db, sql, parameters, delegate (IDataReader reader, Tenant entity)
            {
                BuildTenant(reader, entity);
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Tenant> GetList(Tenant entity)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Tenant>(db, sql.ToString(), parameters, delegate (IDataReader reader, Tenant dataModel)
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
        public List<Tenant> GetList(Tenant entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Tenant>(db, sql.ToString(), parameters, delegate (IDataReader reader, Tenant dataModel)
            {
                BuildTenant(reader, dataModel);
            }, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Tenant entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "Name",entity.Name},
                { "ContactUser",entity.ContactUser},
                { "ContactPhone",entity.ContactPhone},
                { "Address",entity.Address},
                { "Description",entity.Description},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime}
                };
            var filters = new Dictionary<string, object>() {
                { "Id",entity.Id}
            };
            DataHelper.Update(db, tableName, fields, filters);
        }

        #region private

        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        private void BuildTenant(IDataReader reader, Tenant entity)
        {
            entity.Id = reader.GetValue<Guid>("Id");
            entity.Name = reader.GetValue<string>("Name");
            entity.ContactUser = reader.GetValue<string>("ContactUser");
            entity.ContactPhone = reader.GetValue<string>("ContactPhone");
            entity.Address = reader.GetValue<string>("Address");
            entity.Description = reader.GetValue<string>("Description");
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
        private void LoadCondition(Tenant entity, StringBuilder sql, ref Dictionary<string, object> parameters)
        {
            if (entity == null)
                return;

            sql.AppendFormat(" WHERE 1=1 ");

            if (entity.Id != Guid.Empty)
            {
                sql.AppendFormat(" AND [Id]=@Id ");
                parameters.Add("@Id", entity.Id);
            }
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                sql.AppendFormat(" AND [Name] LIKE '%'+@Name+'%' ");
                parameters.Add("@Name", entity.Name);
            }

            if (!string.IsNullOrWhiteSpace(entity.ContactUser))
            {
                sql.AppendFormat(" AND [ContactUser] LIKE '%'+@ContactUser+'%' ");
                parameters.Add("@ContactUser", entity.ContactUser);
            }

            if (!string.IsNullOrWhiteSpace(entity.ContactPhone))
            {
                sql.AppendFormat(" AND [ContactPhone] LIKE '%'+@ContactPhone+'%' ");
                parameters.Add("@ContactPhone", entity.ContactPhone);
            }

            if (!string.IsNullOrWhiteSpace(entity.Address))
            {
                sql.AppendFormat(" AND [Address] LIKE '%'+@Address+'%' ");
                parameters.Add("@Address", entity.Address);
            }

            if (!string.IsNullOrWhiteSpace(entity.Description))
            {
                sql.AppendFormat(" AND [Description] LIKE '%'+@Description+'%' ");
                parameters.Add("@Description", entity.Description);
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