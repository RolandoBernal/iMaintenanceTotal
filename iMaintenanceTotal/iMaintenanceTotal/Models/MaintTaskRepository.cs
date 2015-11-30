using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iMaintenanceTotal.Models
{

    public class MaintTaskRepository : DbContext
    {
        public MaintTaskRepository()
            : base("name=MaintTaskRepository")
        {
        }

    }

}