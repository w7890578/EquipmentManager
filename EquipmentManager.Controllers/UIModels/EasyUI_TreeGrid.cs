using EquipmentManager.Controllers.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EquipmentManager.Controllers.UIModels
{
    public class EasyUI_TreeGrid
    {
        [JsonProperty("children")]
        public List<EasyUI_TreeGrid> Children { get; set; }

        public string Code { get; set; }
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Type { get; set; }
    }
}