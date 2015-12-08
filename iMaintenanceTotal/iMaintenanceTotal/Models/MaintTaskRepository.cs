using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace iMaintenanceTotal.Models
{
    public class MaintTaskRepository
    {
        private MaintTaskContext context;

        public IDbSet<ApplicationUser> Users { get { return context.Users; } }

        public MaintTaskRepository()
        {
            context = new MaintTaskContext();
        }

        public MaintTaskRepository(MaintTaskContext _context)
        {
            context = _context;
        }
    }
}