using System;  

namespace EquipmentManager.Controllers.Models
{
	 	//用户信息
		public class User  
	{
   		     
      	/// <summary>
		/// 主键
        /// </summary>	 
        public Guid Id {  get;set; }        
		/// <summary>
		/// 租户Id
        /// </summary>	 
        public Guid TenantId {  get;set; }        
		/// <summary>
		/// 用户名
        /// </summary>	 
        public string Name {  get;set; }        
		/// <summary>
		/// 密码
        /// </summary>	 
        public string PassWord {  get;set; }        
		/// <summary>
		/// 角色Id
        /// </summary>	 
        public Guid RoleId {  get;set; }        
		/// <summary>
		/// 邮箱
        /// </summary>	 
        public string Email {  get;set; }        
		/// <summary>
		/// 电话
        /// </summary>	 
        public string Phone {  get;set; }        
		/// <summary>
		/// 创建人
        /// </summary>	 
        public Guid CreateBy {  get;set; }        
		/// <summary>
		/// 创建时间
        /// </summary>	 
        public DateTime CreateTime {  get;set; }        
		/// <summary>
		/// 修改人
        /// </summary>	 
        public Guid ModifyBy {  get;set; }        
		/// <summary>
		/// 修改时间
        /// </summary>	 
        public DateTime ModifyTime {  get;set; }        
		   
	}
}

