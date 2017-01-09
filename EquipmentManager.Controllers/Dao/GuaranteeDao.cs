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
    public class GuaranteeDao : BaseDao
    {
        private static string tableName = "Guarantee";

        #region singleton

        private static readonly GuaranteeDao instance = new GuaranteeDao();

        private GuaranteeDao()
        {
            if (db == null)
            {
                db = DataBaseFactory.Create(EquipmentConst.DataBaseName);
            }
        }

        public static GuaranteeDao Instance
        {
            get { return instance; }
        }

        #endregion singleton

        /// <summary>
        ///  创建
        /// </summary>
        /// <param name="entity"></param>
        public void Create(Guarantee entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "Id",entity.Id},
                { "TeantId",entity.TeantId},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime},
                { "EquipmentId",entity.EquipmentId},
                { "Remark",entity.Remark},
                { "StartDateTime",entity.StartDateTime},
                { "EndDateTime",entity.EndDateTime},
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
        public Guarantee GetById(Guid Id)
        {
            string sql = $@"
                SELECT TOP 1 * FROM [dbo].[{tableName}]
                  WHERE [Id]=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                   {"@Id",Id}
            };
            return DataHelper.GetEntity<Guarantee>(db, sql, parameters, delegate (IDataReader reader, Guarantee entity)
            {
                BuildTenant(reader, entity);
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<Guarantee> GetList(Guarantee entity)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Guarantee>(db, sql.ToString(), parameters, delegate (IDataReader reader, Guarantee dataModel)
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
        public List<Guarantee> GetList(Guarantee entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<Guarantee>(db, sql.ToString(), parameters, delegate (IDataReader reader, Guarantee dataModel)
            {
                BuildTenant(reader, dataModel);
            }, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(Guarantee entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "TeantId",entity.TeantId},
                { "CreateBy",entity.CreateBy},
                { "CreateTime",entity.CreateTime},
                { "ModifyBy",entity.ModifyBy},
                { "ModifyTime",entity.ModifyTime},
                { "EquipmentId",entity.EquipmentId},
                { "Remark",entity.Remark},
                { "StartDateTime",entity.StartDateTime},
                { "EndDateTime",entity.EndDateTime},
                { "ComponentId",entity.ComponentId}
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
        private void BuildTenant(IDataReader reader, Guarantee entity)
        {
            entity.Id = reader.GetValue<Guid>("Id");
            entity.TeantId = reader.GetValue<Guid>("TeantId");
            entity.CreateBy = reader.GetValue<Guid>("CreateBy");
            entity.CreateTime = reader.GetValue<DateTime>("CreateTime");
            entity.ModifyBy = reader.GetValue<Guid>("ModifyBy");
            entity.ModifyTime = reader.GetValue<DateTime>("ModifyTime");
            entity.EquipmentId = reader.GetValue<Guid>("EquipmentId");
            entity.Remark = reader.GetValue<string>("Remark");
            entity.StartDateTime = reader.GetValue<DateTime>("StartDateTime");
            entity.EndDateTime = reader.GetValue<DateTime>("EndDateTime");
            entity.ComponentId = reader.GetValue<Guid>("ComponentId");
        }

        /// <summary>
        /// 加载条件
        /// </summary>
        /// <param name="entity">查询实体</param>
        /// <param name="sql">SQL命令</param>
        /// <param name="parameters">参数</param>
        private void LoadCondition(Guarantee entity, StringBuilder sql, ref Dictionary<string, object> parameters)
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

            if (entity.EquipmentId != Guid.Empty)
            {
                sql.AppendFormat(" AND [EquipmentId]=@EquipmentId ");
                parameters.Add("@EquipmentId", entity.EquipmentId);
            }
            if (!string.IsNullOrWhiteSpace(entity.Remark))
            {
                sql.AppendFormat(" AND [Remark]=@Remark ");
                parameters.Add("@Remark", entity.Remark);
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