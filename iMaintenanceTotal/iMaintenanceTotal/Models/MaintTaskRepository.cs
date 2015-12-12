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

        public MaintTask CreateMaintTask(string title, ApplicationUser owner)
        {
            MaintTask my_maintTask = new MaintTask { Title = title, Owner = owner };
            context.MaintTasks.Add(my_maintTask);
            context.SaveChanges(); // This saves something to the Database

            return my_maintTask;
        }

        public virtual List<MaintTask> GetAllBoards()
        {
            return context.MaintTasks.ToList();
        }

        public int GetMaintTasksCount()
        {
            var query = from mt in context.MaintTasks select mt;
            // Same As -> context.Boards.ToList().Count

            return query.Count();
        }

        public bool AddMaintTask(int _maintTask_id, MaintTask _task)
        {
            var query = from mt in context.MaintTasks where mt.MaintTaskId == _maintTask_id select mt;
            MaintTask found_maintTask = null;
            bool result = true;
            try
            {
                found_maintTask = query.Single<MaintTask>();
                _task.CreatedAt = DateTime.Now;
                found_maintTask.CreatedAt.Add(_task);
                context.SaveChanges();
            }
            catch (InvalidOperationException)
            {
                result = false;
            }
            catch (ArgumentNullException)
            {
                result = false;
            }
            return result;
        }
    }
}