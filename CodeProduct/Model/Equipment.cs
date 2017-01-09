using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Controllers.Models
{
    /// <summary>
    /// 
    /// </summary>
	/// <remarks></remarks>
    public class EquipmentModel : BaseModel
    {
				/// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public Guid Id { get; set; }
		/// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public Guid TeantId { get; set; }
		/// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public string Code { get; set; }
		/// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public string Name { get; set; }
		/// <summary>
        /// 
        /// </summary>
        /// <remarks></remarks>
        public string Address { get; set; }

    }
}
