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

        
        public virtual List<MaintTask> GetAllMaintTasks()
        {
            return context.MaintTasks.ToList();
        }
        

        public int GetMaintTasksCount()
        {
            var query = from mt in context.MaintTasks select mt;
            // Same As -> context.Boards.ToList().Count

            return query.Count();
        }

        public bool AddMaintTask(MaintTask _task)
        {
            bool result = true;
            try
            {
                context.MaintTasks.Add(_task);
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


        public void DeleteMaintTask(MaintTask removed_maintTask)
        {
            MaintTask my_maintTask = removed_maintTask;
            context.MaintTasks.Remove(my_maintTask);
            context.SaveChanges();
        }

        public MaintTask UpdateMaintTask(string title)
        {
            var query = context.MaintTasks.Where(mt => mt.Title == title);
            var result = query.First();

            context.SaveChanges();
            return result;
        }

        public MaintTask GetMaintTaskById(int id)
        {
            var maintTask = (from mt in context.MaintTasks where mt.MaintTaskId == id select mt).First();
            return maintTask;
        }

        public List<MaintTask> GetMaintTasks(ApplicationUser user1)
        {
            var query = from mt in context.MaintTasks where mt.Owner.Id == user1.Id select mt;
            return query.ToList<MaintTask>(); // Same as query.ToList();
        }

    }
}