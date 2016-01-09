using iMaintenanceTotal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;

namespace iMaintenanceTotal.Controllers
{
    public class MaintTaskController : Controller
    {
        private MaintTaskRepository repository;
        //private Mock<>
        private MaintTaskContext db = new MaintTaskContext();

        public MaintTaskController()
        {
            repository = new MaintTaskRepository();
        }

        public MaintTaskController(MaintTaskRepository _repo)
        {
            repository = _repo;
        }

        // GET: MaintTask
        public ActionResult Index()
        {
            string user_id = User.Identity.GetUserId();
            ApplicationUser me = repository.Users.FirstOrDefault(u => u.Id == user_id);
            ViewBag.DisplayName = me.DisplayName;
            var maintTasks = repository.GetMaintTasks(me);
            return View(maintTasks);
        }

        // GET: MaintTask/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: MaintTask/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MaintTask/Create
        [HttpPost]
        public ActionResult CreateMaintTask(FormCollection collection)
        {
            try
            {
                // Added insert logic here
                //string mt_id = collection.Get("mt-id");
                string mt_title = collection.Get("mt-title");
                string mt_complete_by = collection.Get("mt-complete-by");
                string mt_frequency = collection.Get("mt-frequency");
                string mt_notes = collection.Get("mt-notes");
                string mt_category = collection.Get("mt-category");
                string mt_remind_me_on = collection.Get("mt-remind-me-on");
                string mt_remind_me_by = "Phone"; //collection.Get("mt-remind-me-by");
                DateTime mt_remind_me_on_date = DateTime.Parse(mt_remind_me_on);
                DateTime mt_complete_by_date = DateTime.Parse(mt_complete_by);
                string user_id = User.Identity.GetUserId();
                ApplicationUser me = repository.Users.FirstOrDefault(u => u.Id == user_id);

                //MaintTask current_m_task = repository.GetMaintTaskById(int.Parse(mt_id));
                repository.AddMaintTask(new MaintTask {
                    Title = mt_title,
                    CreatedAt = DateTime.Now,
                    CompleteBy = mt_complete_by_date,
                    Frequency = mt_frequency,
                    Notes = mt_notes,
                    RemindMeOn = mt_remind_me_on_date,
                    RemindMeBy = mt_remind_me_by,
                    Category = mt_category,
                    Owner = me
                });

                return RedirectToAction("Index");
                //return View();
            }
            catch (Exception e)
            {
                //return View();
                return RedirectToAction("Index");
            }
        }

        // POST: MaintTask/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(MaintTask input)
        {
            if (TryValidateModel(input))
            {
                var CreatedAt = DateTime.Now;
                db.Entry(input).State = EntityState.Modified;
                db.SaveChanges();
                return PartialView("dataRowView", (MaintTask)input);
            }

            Response.StatusCode = 400;
            return PartialView("ModalPartial", (MaintTask)input);
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
            public string Category { get; set; }
            public ApplicationUser Owner { get; set; }
            */
            /*
            try
            {
                MaintTask item_to_edit = repository.GetMaintTaskById(id);
                if (TryUpdateModel(item_to_edit, "", new string[] { "Title", "CompleteBy", "Frequency", "Notes", "RemindMeOn", "Category" }))
                {
                    db.SaveChanges();
                };
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            } */
        }

        // GET: MaintTask/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MaintTask item_to_edit = db.MaintTasks.Find(id);
            if (item_to_edit == null)
            {
                return HttpNotFound();
            }
            return PartialView("ModalPartial", item_to_edit);
        }


        // GET: MaintTask/Delete/5
        [Authorize]
        public ActionResult Delete(int id)
        {
            MaintTask item_to_delete = repository.GetMaintTaskById(id);
            //return View(item_to_delete);
            return RedirectToAction("Index");
        }

        // POST: MaintTask/Delete/5
        [HttpPost, Authorize]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                repository.DeleteMaintTask(id);
                return RedirectToAction("Index");
            }
            catch
            {
                //return View();
                return RedirectToAction("Index");
            }
        }
    }
}
