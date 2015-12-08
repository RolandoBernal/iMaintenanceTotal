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


        [TestMethod]
        public void MaintTaskEnsurePropertiesWork()
        {
            //Begin Arrange
            MaintTask m_task = new MaintTask
            {
                Title = "Change Air Filters",
                CreatedAt = DateTime.Now,
                CompleteBy = DateTime.Now + new TimeSpan(10, 10, 10, 10),
                Completed = false,
                Frequency = "Every Year",
                Notes = "Buy big filters",
                RemindMeOn = DateTime.Parse("2015-12-30"),
                RemindMeBy = "Phone",
            };
            //End Arrange

            //Begin Act
            //End Act

            //Begin Assert
            Assert.AreEqual("Change Air Filters", m_task.Title);
            Assert.AreEqual(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), m_task.CreatedAt.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual((DateTime.Now + new TimeSpan(10, 10, 10, 10)).ToString("yyyy-MM-dd HH:mm:ss"), m_task.CompleteBy.ToString("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual(false, m_task.Completed);
            Assert.AreEqual("Every Year", m_task.Frequency);
            Assert.AreEqual("Buy big filters", m_task.Notes);
            Assert.AreEqual(DateTime.Parse("2015-12-30"), m_task.RemindMeOn);
            Assert.AreEqual("Phone", m_task.RemindMeBy);
            //End Assert
        }
            
    }
}


/*
            Color color = new Color { Name = "Blue", Value = "#0000ff" };
            // Object Initializer syntax
            Card c = new Card { Title = "My Card", Description = "A description of my card", BorderColor = color};
            // Otherwise you'd have to
            Assert.AreEqual("My Card", c.Title);
            Assert.AreEqual("A description of my card", c.Description);
            Assert.AreEqual("Blue", c.BorderColor.Name);

*/
