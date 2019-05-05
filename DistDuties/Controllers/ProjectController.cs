using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DistDuties.DAL;
using DistDuties.Models;

namespace DistDuties.Controllers
{
    public class ProjectController : Controller
    {
        private DistContext db = new DistContext();

        // GET: Project
        public ActionResult Index()
        {
            return View(db.Projects.ToList());
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
