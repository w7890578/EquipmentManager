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
    public class FileDao : BaseDao
    {
        private static string tableName = "File";

        #region singleton

        private static readonly FileDao instance = new FileDao();

        private FileDao()
        {
            if (db == null)
            {
                db = DataBaseFactory.Create(EquipmentConst.DataBaseName);
            }
        }

        public static FileDao Instance
        {
            get { return instance; }
        }

        #endregion singleton

        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(File entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "Id",entity.Id},
                { "TenantId",entity.TenantId},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime},
                { "Link",entity.Link},
                { "Type",entity.Type},
                { "Name",entity.Name},
                { "Remark",entity.Remark},
                { "Size",entity.Size},
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
        public File GetById(Guid Id)
        {
            string sql = $@"
                SELECT TOP 1 * FROM [dbo].[{tableName}]
                  WHERE [Id]=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                   {"@Id",Id}
            };
            return DataHelper.GetEntity<File>(db, sql, parameters, delegate (IDataReader reader, File entity)
            {
                BuildTenant(reader, entity);
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<File> GetList(File entity)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<File>(db, sql.ToString(), parameters, delegate (IDataReader reader, File dataModel)
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
        public List<File> GetList(File entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<File>(db, sql.ToString(), parameters, delegate (IDataReader reader, File dataModel)
            {
                BuildTenant(reader, dataModel);
            }, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(File entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "TenantId",entity.TenantId},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime},
                { "Link",entity.Link},
                { "Type",entity.Type},
                { "Name",entity.Name},
                { "Remark",entity.Remark},
                { "Size",entity.Size},
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
        private void BuildTenant(IDataReader reader, File entity)
        {
            entity.Id = reader.GetValue<Guid>("Id");
            entity.TenantId = reader.GetValue<Guid>("TenantId");
            entity.CreateBy = reader.GetValue<Guid>("CreateBy");
            entity.CreateTime = reader.GetValue<DateTime>("CreateTime");
            entity.ModifyBy = reader.GetValue<Guid>("ModifyBy");
            entity.ModifyTime = reader.GetValue<DateTime>("ModifyTime");
            entity.Link = reader.GetValue<string>("Link");
            entity.Type = reader.GetValue<string>("Type");
            entity.Name = reader.GetValue<string>("Name");
            entity.Remark = reader.GetValue<string>("Remark");
            entity.Size = reader.GetValue<decimal>("Size");
            entity.EquipmentId = reader.GetValue<Guid>("EquipmentId");
            entity.ComponentId = reader.GetValue<Guid>("ComponentId");
        }

        /// <summary>
        /// 加载条件
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="parameters">参数</param>
        private void LoadCondition(File entity, StringBuilder sql, ref Dictionary<string, object> parameters)
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

            if (!string.IsNullOrWhiteSpace(entity.Link))
            {
                sql.AppendFormat(" AND [Link]=@Link ");
                parameters.Add("@Link", entity.Link);
            }

            if (!string.IsNullOrWhiteSpace(entity.Type))
            {
                sql.AppendFormat(" AND [Type]=@Type ");
                parameters.Add("@Type", entity.Type);
            }

            if (!string.IsNullOrWhiteSpace(entity.Name))
            {
                sql.AppendFormat(" AND [Name]=@Name ");
                parameters.Add("@Name", entity.Name);
            }

            if (!string.IsNullOrWhiteSpace(entity.Remark))
            {
                sql.AppendFormat(" AND [Remark]=@Remark ");
                parameters.Add("@Remark", entity.Remark);
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