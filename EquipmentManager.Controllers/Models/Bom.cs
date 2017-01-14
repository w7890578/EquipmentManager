using System;

namespace EquipmentManager.Controllers.Models
{
    //Bom
    public class Bom
    {
        /// <summary>
        /// 组件Id
        /// </summary>
        public Guid ComponentId { get; set; }

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
        /// 组件型号
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public Guid ModifyBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime ModifyTime { get; set; }

        /// <summary>
        /// 组件名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 单机用量
        /// </summary>
        public decimal UnitSingleUnit { get; set; }
    }
}