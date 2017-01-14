using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EquipmentManager.Controllers.Dao;

namespace EquipmentManager.Test.Dao
{
    [TestClass]
    public class OrganizationDaoTest
    {
        [TestMethod]
        public void GetTreeTest()
        {
            Guid tenantId = Guid.Empty;
            Guid parentId = new Guid("503240C8-3C45-4C1E-9834-E5CA34FC059C");
            var result = OrganizationDao.Instance.GetTreeAll(tenantId, parentId);
        }
    }
}