using System;

namespace EquipmentManager.Controllers.Models
{
    //组织机构和设备的关联表
    public class Re_Organization_Equipment
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
        /// 设备Id
        /// </summary>
        public Guid EquipmentId { get; set; }

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
        /// 组织机构Id
        /// </summary>
        public Guid OrganizationId { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }
    }
}