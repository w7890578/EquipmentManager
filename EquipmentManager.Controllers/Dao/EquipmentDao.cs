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
    public class EquipmentDao : BaseDao
    {
        private static string tableName = "Equipment";

        #region singleton

        private static readonly EquipmentDao instance = new EquipmentDao();

        private EquipmentDao()
        {
            if (db == null)
            {
                db = DataBaseFactory.Create(EquipmentConst.DataBaseName);
            }
        }

        public static EquipmentDao Instance
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
            var fields = new Dictionary<string, object>()
            {
                { "Id",entity.Id},
                { "TeantId",entity.TeantId},
                { "Name",entity.Name},
                { "Code",entity.Code},
                { "Classify",entity.Classify},
                { "Address",entity.Address},
                { "Description",entity.Description},
                { "CreateTime",entity.CreateTime},
                { "CreateBy",entity.CreateBy},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime},
                { "ImageLink",entity.ImageLink}
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
        public Equipment GetById(Guid Id)
        {
            string sql = $@"
                SELECT TOP 1 * FROM [dbo].[{tableName}]
                  WHERE [Id]=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                   {"@Id",Id}
            };
            return DataHelper.GetEntity<Equipment>(db, sql, parameters, delegate (IDataReader reader, Equipment entity)
            {
                BuildTenant(reader, entity);
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Equipment> GetList(Equipment entity)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Equipment>(db, sql.ToString(), parameters, delegate (IDataReader reader, Equipment dataModel)
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
        public List<Equipment> GetList(Equipment entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Equipment>(db, sql.ToString(), parameters, delegate (IDataReader reader, Equipment dataModel)
            {
                BuildTenant(reader, dataModel);
            }, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Equipment entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "TeantId",entity.TeantId},
                { "Name",entity.Name},
                { "Code",entity.Code},
                { "Classify",entity.Classify},
                { "Address",entity.Address},
                { "Description",entity.Description},
                { "CreateTime",entity.CreateTime},
                { "CreateBy",entity.CreateBy},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime},
                { "ImageLink",entity.ImageLink}
           };
            var filters = new Dictionary<string, object>() {
                { "Id",entity.Id},
                { "TeantId",entity.TeantId}
            };
            DataHelper.Update(db, tableName, fields, filters);
        }

        #region private

        /// <summary>
        /// 映射
        /// </summary>
        /// <param name="reader"></param>
        /// <param name="entity"></param>
        private void BuildTenant(IDataReader reader, Equipment entity)
        {
            entity.Id = reader.GetValue<Guid>("Id");
            entity.TeantId = reader.GetValue<Guid>("TeantId");
            entity.Name = reader.GetValue<string>("Name");
            entity.Code = reader.GetValue<string>("Code");
            entity.Classify = reader.GetValue<string>("Classify");
            entity.Address = reader.GetValue<string>("Address");
            entity.Description = reader.GetValue<string>("Description");
            entity.CreateTime = reader.GetValue<DateTime>("CreateTime");
            entity.CreateBy = reader.GetValue<Guid>("CreateBy");
            entity.ModifyBy = reader.GetValue<Guid>("ModifyBy");
            entity.ModifyTime = reader.GetValue<DateTime>("ModifyTime");
            entity.ImageLink = reader.GetValue<string>("ImageLink");
        }

        /// <summary>
        /// 加载条件
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="parameters">参数</param>
        private void LoadCondition(Equipment entity, StringBuilder sql, ref Dictionary<string, object> parameters)
        {
            if (entity == null)
                return;

            sql.AppendFormat(" WHERE 1=1 ");

            if (entity.Id != Guid.Empty)
            {
                sql.AppendFormat(" AND [Id]=@Id ");
                parameters.Add("@Id", entity.Id);
            }
            if (entity.TeantId != Guid.Empty)
            {
                sql.AppendFormat(" AND [TeantId]=@TeantId ");
                parameters.Add("@TeantId", entity.TeantId);
            }
            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                sql.AppendFormat(" AND [Name] LIKE '%'+@Name+'%' ");
                parameters.Add("@Name", entity.Name);
            }

            if (!string.IsNullOrWhiteSpace(entity.Code))
            {
                sql.AppendFormat(" AND [Code] LIKE '%'+@Code+'%' ");
                parameters.Add("@Code", entity.Code);
            }

            if (!string.IsNullOrWhiteSpace(entity.Classify))
            {
                sql.AppendFormat(" AND [Classify] LIKE '%'+@Classify+'%' ");
                parameters.Add("@Classify", entity.Classify);
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

            if (!string.IsNullOrWhiteSpace(entity.ImageLink))
            {
                sql.AppendFormat(" AND [ImageLink] LIKE '%'+@ImageLink+'%' ");
                parameters.Add("@ImageLink", entity.ImageLink);
            }
        }

        #endregion private
    }
}