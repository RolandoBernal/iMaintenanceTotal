using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iMaintenanceTotal.Controllers;
using System.Web.Mvc;
using Moq;
using iMaintenanceTotal.Models;
using System.Collections.Generic;

namespace iMaintenanceTotal.Tests.Controllers
{
    [TestClass]
    public class MaintTaskControllerTests
    {
        private Mock<MaintTaskContext> mock_context;
        private Mock<MaintTaskRepository> mock_repository;
        private ApplicationUser owner, user1, user2;

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<MaintTaskContext>();
            mock_repository = new Mock<MaintTaskRepository>(MockBehavior.Strict, mock_context.Object);
            owner = new ApplicationUser();
            user1 = new ApplicationUser();
            user2 = new ApplicationUser();
        }

        [TestMethod]
        public void MaintTaskControllerEnsureIndexPageExists()
        {
            // Arrange
            MaintTaskController controller = new MaintTaskController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void MaintTaskControllerEnsureIndexViewExists()
        {
            // Arrange
            MaintTaskController controller = new MaintTaskController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            Assert.AreEqual("Index", result.ViewName);
        }

        [TestMethod]
        public void MaintTaskControllerEnsureItHasThings()
        {
            // Arrange
            MaintTaskController controller = new MaintTaskController();
            // Act
            ViewResult result = controller.Index() as ViewResult;
            // Assert
            string expected_message = "My Maint Task Reminders";
            Assert.AreEqual(expected_message, result.ViewBag.Message);
        }

        [TestMethod]
        public void MaintTaskControllerEnsureListOfUserMaintTasks()
        {
            // Arrange
            List<MaintTask> data_store_m_tasks = new List<MaintTask>
            {
                new MaintTask {Title = "My Maint Task", MaintTaskId = 1, Owner = owner  },
                new MaintTask {Title = "My Maint Task 2", MaintTaskId = 2, Owner = owner  }
            };
            MaintTaskController controller = new MaintTaskController(mock_repository.Object);
            mock_repository.Setup(r => r.GetAllMaintTasks()).Returns(data_store_m_tasks);
            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            CollectionAssert.AreEqual(data_store_m_tasks, result.ViewBag.MaintTasks);
        }
    }
}
