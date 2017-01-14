using System;

namespace EquipmentManager.Controllers.Models
{
    //资产信息
    public class Component
    {
        /// <summary>
        /// 资产编码
        /// </summary>
        public string Code { get; set; }

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
        /// 设备价值
        /// </summary>
        public decimal EquipmentValue { get; set; }

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 型号
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
        /// 资产名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 父Id
        /// </summary>
        public Guid ParentId { get; set; }

        /// <summary>
        /// 采购日期
        /// </summary>
        public DateTime PurchaseDateTime { get; set; }

        /// <summary>
        /// 序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TenantId { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public int Type { get; set; }
    }
}