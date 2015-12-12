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
        private IEnumerable<MaintTask> _maintTasks;

        private List<MaintTask> GetMaintTask()
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

        }

        [TestInitialize]
        public void Initialize()
        {
            var mock_context = new Mock<MaintTaskContext>();
            _maintTasks = GetMaintTask();


        }
    }
}
