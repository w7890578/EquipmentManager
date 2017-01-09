using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(EquipmentManager.Startup))]

namespace EquipmentManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}