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
    public class UserDao : BaseDao
    {
        private static string tableName = "User";

        #region singleton

        private static readonly UserDao instance = new UserDao();

        private UserDao()
        {
            if (db == null)
            {
                db = DataBaseFactory.Create("Equipment");
            }
        }

        public static UserDao Instance
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
            var fields = new Dictionary<string, object>()
            {
                { "Id",entity.Id},
                { "TenantId",entity.TenantId},
                { "Name",entity.Name},
                { "PassWord",entity.PassWord},
                { "RoleId",entity.RoleId},
                { "Email",entity.Email},
                { "Phone",entity.Phone},
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
        public User GetById(Guid Id)
        {
            string sql = $@"
                SELECT TOP 1 * FROM [dbo].[{tableName}]
                  WHERE [Id]=@Id";
            Dictionary<string, object> parameters = new Dictionary<string, object>()
            {
                   {"@Id",Id}
            };
            return DataHelper.GetEntity<User>(db, sql, parameters, delegate (IDataReader reader, User entity)
            {
                BuildTenant(reader, entity);
            });
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public List<User> GetList(User entity)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<User>(db, sql.ToString(), parameters, delegate (IDataReader reader, User dataModel)
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
        public List<User> GetList(User entity, int pageIndex, int pageSize, string order = EquipmentConst.Order)
        {
            StringBuilder sql = new StringBuilder($" SELECT * FROM [dbo].[{tableName}] ");
            Dictionary<string, object> parameters = new Dictionary<string, object>();

            LoadCondition(entity, sql, ref parameters);
            return DataHelper.GetList<User>(db, sql.ToString(), parameters, delegate (IDataReader reader, User dataModel)
            {
                BuildTenant(reader, dataModel);
            }, pageIndex, pageSize, order);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity"></param>
        public void Update(User entity)
        {
            var fields = new Dictionary<string, object>()
            {
                { "TenantId",entity.TenantId},
                { "Name",entity.Name},
                { "PassWord",entity.PassWord},
                { "RoleId",entity.RoleId},
                { "Email",entity.Email},
                { "Phone",entity.Phone},
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
        private void BuildTenant(IDataReader reader, User entity)
        {
            entity.Id = reader.GetValue<Guid>("Id");
            entity.TenantId = reader.GetValue<Guid>("TenantId");
            entity.Name = reader.GetValue<string>("Name");
            entity.PassWord = reader.GetValue<string>("PassWord");
            entity.RoleId = reader.GetValue<Guid>("RoleId");
            entity.Email = reader.GetValue<string>("Email");
            entity.Phone = reader.GetValue<string>("Phone");
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
        private void LoadCondition(User entity, StringBuilder sql, ref Dictionary<string, object> parameters)
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

            if (!string.IsNullOrWhiteSpace(entity.PassWord))
            {
                sql.AppendFormat(" AND [PassWord] LIKE '%'+@PassWord+'%' ");
                parameters.Add("@PassWord", entity.PassWord);
            }

            if (entity.RoleId != Guid.Empty)
            {
                sql.AppendFormat(" AND [RoleId]=@RoleId ");
                parameters.Add("@RoleId", entity.RoleId);
            }
            if (!string.IsNullOrWhiteSpace(entity.Email))
            {
                sql.AppendFormat(" AND [Email] LIKE '%'+@Email+'%' ");
                parameters.Add("@Email", entity.Email);
            }

            if (!string.IsNullOrWhiteSpace(entity.Phone))
            {
                sql.AppendFormat(" AND [Phone] LIKE '%'+@Phone+'%' ");
                parameters.Add("@Phone", entity.Phone);
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