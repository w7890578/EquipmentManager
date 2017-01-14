using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace EquipmentManager.Controllers.Models
{
    //组织信息
    public class Organization
    {
        /// <summary>
        /// 创建人
        /// </summary>
        public Guid CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public Guid ModifyBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }
    }
}