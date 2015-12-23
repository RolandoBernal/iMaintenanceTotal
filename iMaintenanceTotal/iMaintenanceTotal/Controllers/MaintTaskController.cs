using iMaintenanceTotal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace iMaintenanceTotal.Controllers
{
    public class MaintTaskController : Controller
    {
        private MaintTaskRepository repository;
        //private Mock<>

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
            var maintTasks = repository.GetAllMaintTasks();
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
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MaintTask/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MaintTask/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: MaintTask/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: MaintTask/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
