using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace iMaintenanceTotal.Models
{
    public class MaintTaskContext : ApplicationDbContext
    {
        public IDbSet<MaintTask> MaintTasks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}