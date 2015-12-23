using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using iMaintenanceTotal.Models;
using Moq;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;

namespace iMaintenanceTotal.Tests.Models
{
    [TestClass]
    public class MaintTaskRepositoryTests
    {
        /*
                public int MaintTaskId { get; set; }
                public string Title { get; set; }
                public DateTime CreatedAt { get; set; }
                public DateTime CompleteBy { get; set; }
                public bool Completed { get; set; }
                public string Frequency { get; set; }
                public string Notes { get; set; }
                public DateTime RemindMeOn { get; set; }
                public string RemindMeBy { get; set; }
                public ApplicationUser Owner { get; set; }
        */

        private IEnumerable<MaintTask> _maintTasks;
        private Mock<MaintTaskContext> mock_context;
        private Mock<IDbSet<MaintTask>> mock_maintTasks;
        private List<MaintTask> my_maintTasks;
        public ApplicationUser owner, user1;

        private List<MaintTask> MaintTaskRepositoryEnsureICanGetMaintTask()
        {
            List<MaintTask> new_list = new List<MaintTask>();
            new_list.Add(new MaintTask
                {
                    MaintTaskId = 1, Title = "MyFirstTask"
                }
            );
            new_list.Add(new MaintTask
                {
                    MaintTaskId = 2,
                    Title = "MySecondTask"
                }
            );

            return new_list;
            

        }

        private void ConnectMocksToDataSource()
        {
            // This setups the Mocks and connects to the Data Source (my_list in this case)
            var data = my_maintTasks.AsQueryable();

            mock_maintTasks.As<IQueryable<MaintTask>>().Setup(m => m.Provider).Returns(data.Provider);
            mock_maintTasks.As<IQueryable<MaintTask>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
            mock_maintTasks.As<IQueryable<MaintTask>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mock_maintTasks.As<IQueryable<MaintTask>>().Setup(m => m.Expression).Returns(data.Expression);

            // This allows MaintTaskRepository to call MaintTasks.Add and have it update the my_maintTasks instance and Enumerator
            // Connect DbSet.Add to List.Add so they work together
            mock_maintTasks.Setup(m => m.Add(It.IsAny<MaintTask>())).Callback((MaintTask mt) => my_maintTasks.Add(mt));
            mock_maintTasks.Setup(m => m.Remove(It.IsAny<MaintTask>())).Callback((MaintTask mt) => my_maintTasks.Remove(mt));

            mock_context.Setup(m => m.MaintTasks).Returns(mock_maintTasks.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            mock_context = new Mock<MaintTaskContext>();
            _maintTasks = MaintTaskRepositoryEnsureICanGetMaintTask();
            mock_maintTasks = new Mock<IDbSet<MaintTask>>();
            my_maintTasks = new List<MaintTask>();
            owner = new ApplicationUser();
        }

        [TestCleanup]
        public void Cleanup()
        {
            mock_context = null;
            mock_maintTasks = null;
            my_maintTasks = null;
        }


        [TestMethod]
        public void MaintTaskRepositoryEnsureICanCreateAMaintTask()
        {
            MaintTask maintTask = new MaintTask();
            Assert.IsNotNull(maintTask);
        }

        [TestMethod]
        public void MaintTaskRepositoryEnsurePropertiesWork()
        {
            //Begin Arrange
            MaintTask maintTaskContent = new MaintTask { MaintTaskId = 1, Title = "Change Air Filters", Frequency = "Every Year" };
                
            //End Arrange

            //Begin Act
            //End Act

            //Begin Assert
            Assert.AreEqual(1, maintTaskContent.MaintTaskId);
            Assert.AreEqual("Change Air Filters", maintTaskContent.Title);
            Assert.AreEqual("Every Year", maintTaskContent.Frequency);
            //End Assert
        }

        [TestMethod]
        public void MaintTaskRepositoryEnsureICanAddAMaintTask()
        {
            MaintTaskRepository maintTask_repo = new MaintTaskRepository(mock_context.Object);
            MaintTask list = new MaintTask { Title = "Change Air Filters", MaintTaskId = 1, Frequency = "Every Year" };
            my_maintTasks.Add(new MaintTask { Title = "My First Task", Owner = user1 });

            ConnectMocksToDataSource();

            bool actual = maintTask_repo.AddMaintTask(list);

            Assert.AreEqual(1, maintTask_repo.GetAllMaintTasks().Count);
            Assert.IsTrue(actual);
        }


        [TestMethod]
        public void MaintTaskRepositoryEnsureICanDeleteAMaintTask()
        { 
            // Arrange
            MaintTaskRepository maintTask_repo = new MaintTaskRepository(mock_context.Object);
            MaintTask m_task1 = new MaintTask()
            {
                MaintTaskId = 1,
                Title = "my maint task",
                Frequency = "Every Year"
            };
            MaintTask m_task2 = new MaintTask()
            {
                MaintTaskId = 2,
                Title = "my maint task 2",
                Frequency = "Every Year"
            };

            my_maintTasks.Add(m_task1);
            my_maintTasks.Add(m_task2);

            ConnectMocksToDataSource();

            // Act
            bool success = maintTask_repo.DeleteMaintTask(m_task1.MaintTaskId);
            MaintTask m_task_found1 = maintTask_repo.GetMaintTaskById(m_task1.MaintTaskId);
            MaintTask m_task_found2 = maintTask_repo.GetMaintTaskById(m_task2.MaintTaskId);

            // Assert
            Assert.IsTrue(success);
            //Assert.IsNull(m_task_found1);
            //Assert.AreEqual(1, maintTask_repo.GetAllMaintTasks().Count);
            Assert.AreEqual(1, maintTask_repo.MaintTasks.Count());
            Assert.AreEqual("my maint task 2", m_task_found2.Title);
        }

        [TestMethod]
        public void MaintTaskRepositoryEnsureICanEditAMaintTask()
        {
            // Arrange
            MaintTaskRepository maintTask_repo = new MaintTaskRepository(mock_context.Object);
            MaintTask m_task1 = new MaintTask { MaintTaskId = 1, Title = "My Maint Task" };
            MaintTask m_task2 = new MaintTask { MaintTaskId = 2, Title = "My Maint Task 2" };

            my_maintTasks.Add(m_task1);
            my_maintTasks.Add(m_task2);

            ConnectMocksToDataSource();

            // Act
            var newTitle1 = "My Maint Task NEW 1";
            var newTitle2 = "My Maint Task NEW 2";
            m_task1.Title = newTitle1;
            m_task2.Title = newTitle2;
            var mt_success1 = maintTask_repo.UpdateMaintTask(m_task1);
            var mt_success2 = maintTask_repo.UpdateMaintTask(m_task2);
            var actual1 = maintTask_repo.GetMaintTaskById(m_task1.MaintTaskId);
            var actual2 = maintTask_repo.GetMaintTaskById(m_task2.MaintTaskId);

            // Assert
            Assert.AreEqual(newTitle1, actual1.Title);
            Assert.AreEqual(newTitle2, actual2.Title);
        }





    }
}
