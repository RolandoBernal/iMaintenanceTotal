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

            mock_context.Setup(m => m.MaintTasks).Returns(mock_maintTasks.Object);
        }

        [TestInitialize]
        public void Initialize()
        {
            var mock_context = new Mock<MaintTaskContext>();
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
            //Begin Arrange
            my_maintTasks.AsQueryable();
            string title = "Clean Air Filters";

            ConnectMocksToDataSource();
            mock_maintTasks.Setup(m => m.Add(It.IsAny<MaintTask>())).Callback((MaintTask mt) => my_maintTasks.Add(mt));
            mock_maintTasks.Setup(m => m.Remove(It.IsAny<MaintTask>())).Callback((MaintTask mt) => my_maintTasks.Remove(mt));

            MaintTaskRepository repo = new MaintTaskRepository(mock_context.Object);
            //End Arrange

            //Begin Act
            MaintTask removed_maintTask = repo.CreateMaintTask(title, owner);
            //End Act

            //Begin Assert
            Assert.IsNotNull(removed_maintTask);
            mock_maintTasks.Verify(m => m.Add(It.IsAny<MaintTask>()));
            //mock_context.Verify(x => x.SaveChanges(), Times.Once());
            Assert.AreEqual(1, repo.GetMaintTasksCount());
            repo.DeleteMaintTask(removed_maintTask);
            mock_maintTasks.Verify(x => x.Remove(It.IsAny<MaintTask>()));
            //mock_context.Verify(x => x.SaveChanges(), Times.Exactly(2));
            Assert.AreEqual(0, repo.GetMaintTasksCount());

            //End Assert
        }

        [TestMethod]
        public void ListEnsureICanEditAProject()
        {
            //Begin Arrange
            my_maintTasks.AsQueryable();

            mock_maintTasks.Setup(m => m.Add(It.IsAny<MaintTask>())).Callback((MaintTask mt) => my_maintTasks.Add(mt));


            MaintTaskRepository repo = new MaintTaskRepository(mock_context.Object);


            //End Arrange

            MaintTask maintTaskToChange = repo.CreateMaintTask("Clean Gutters", owner);
            //Begin Act

            // Begin Assert
            Assert.IsNotNull(maintTaskToChange);
            Assert.AreEqual(1, repo.GetMaintTasksCount());

            maintTaskToChange.Title = "Cut the grass";

            repo.UpdateMaintTask(maintTaskToChange.Title);

            //End Act

            //Begin Assert
            var updatedProject = repo.GetMaintTaskById(maintTaskToChange.MaintTaskId);

            //Assert.AreEqual(updatedProject.ProjectName, "blah blah");
            //End Assert
        }





    }
}
