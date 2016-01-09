using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using iMaintenanceTotal.Models;

namespace iMaintenanceTotal.Models
{
    public class MaintTaskRepository
    {
        private MaintTaskContext context;

        public IDbSet<ApplicationUser> Users { get { return context.Users; } }
        public IDbSet<MaintTask> MaintTasks { get { return context.MaintTasks; } }

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


        public bool DeleteMaintTask(int maintTaskId)
        {
            var result = true;
            try
            {
                //var query = context.MaintTasks.Where(mt => mt.MaintTaskId == maintTaskId);
                var query = from mt in context.MaintTasks where mt.MaintTaskId == maintTaskId select mt;
                var maintTaskToDelete = query.SingleOrDefault();
                context.MaintTasks.Remove(maintTaskToDelete);
                context.SaveChanges();
            }
            catch (Exception)
            {
                result = false;
                throw;
            }
            return result;
        }

        public bool UpdateMaintTask(int MaintTaskId, string newTitle)
        {
            var success = true;
            try
            {
                var query = context.MaintTasks.Where(mt => mt.MaintTaskId == MaintTaskId);
                var result = query.First();
                result.Title = newTitle;
                context.SaveChanges();
            }
            catch (Exception)
            {
                success = false;
            }
            return success;
        }

        public MaintTask UpdateMaintTask(MaintTask updatedMaintTask)
        {
            var query = (from mt in context.MaintTasks
                         where mt.MaintTaskId == updatedMaintTask.MaintTaskId
                         select mt);
            var mTaskToUpdate = query.First();
            mTaskToUpdate.Title = updatedMaintTask.Title;
            mTaskToUpdate.Notes = updatedMaintTask.Notes;
            mTaskToUpdate.Frequency = updatedMaintTask.Frequency;
            mTaskToUpdate.RemindMeBy = updatedMaintTask.RemindMeBy;
            mTaskToUpdate.RemindMeOn = updatedMaintTask.RemindMeOn;
            context.SaveChanges();
            return mTaskToUpdate;
        }

        public MaintTask GetMaintTaskById(int id)
        {
            var maintTask = (from mt in context.MaintTasks where mt.MaintTaskId == id select mt).SingleOrDefault();
            return maintTask;
        }

        public List<MaintTask> GetMaintTasks(ApplicationUser user1)
        {
            var query = from mt in context.MaintTasks where mt.Owner.Id == user1.Id select mt;
            return query.ToList<MaintTask>(); // Same as query.ToList();
        }


    }
}