using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace iMaintenanceTotal.Models
{
    public class MaintTaskContext : DbContext
    {
        public IDbSet<ApplicationUser> Users { get; internal set; }
    }
}