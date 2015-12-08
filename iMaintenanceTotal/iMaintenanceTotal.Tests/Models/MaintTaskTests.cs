using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iMaintenanceTotal.Models;

namespace iMaintenanceTotal.Tests.Models
{
    [TestClass]
    public class MaintTaskTests
    {
        [TestMethod]
        public void MaintTaskEnsureICanCreateAnInstance()
        {
            MaintTask m_task = new MaintTask();
            Assert.IsNotNull(m_task);
        }

        
    }
}
