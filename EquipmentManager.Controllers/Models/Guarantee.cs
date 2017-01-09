using System;

namespace EquipmentManager.Controllers.Models
{
    //保修
    public class Guarantee
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
        /// 结束日期
        /// </summary>
        public DateTime EndDateTime { get; set; }

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
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 开始日期
        /// </summary>
        public DateTime StartDateTime { get; set; }

        /// <summary>
        /// 租户Id
        /// </summary>
        public Guid TeantId { get; set; }
    }
}