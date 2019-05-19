using DistDuties.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DistDuties.Models;

namespace DistDuties.Controllers
{
    public class TaskoController : Controller
    {

        private DistContext db = new DistContext();

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create(int? projectId)
        {
            if (projectId != null)
            {
                bool isProjectExists = db.Projects.Any(a => a.ProjectID == projectId);

                if (isProjectExists)
                {
                    var project = db.Projects.FirstOrDefault(a => a.ProjectID == projectId && a.CreatorEmail == User.Identity.Name);

                    if (project == null)
                    {
                        return View("~/Views/Shared/Error.cshtml");
                    }

                    return View();

                }
            }

            return View("~/Views/Shared/Error.cshtml");

        }

        [HttpPost]
        public ActionResult Create(ProjectTask projectTask)
        {
            if (ModelState.IsValid)
            {
                TeamMate teamMate = db.TeamMates.FirstOrDefault(a => a.Email == projectTask.TeamMateEmail && a.ProjectID == projectTask.ProjectID);

                if(teamMate != null)
                {
                    if (projectTask.ProjectID == teamMate.ProjectID)
                    {
                        projectTask.TeamMateID = teamMate.TeamMateID;
                        projectTask.Status = TaskStatus.New;
                        db.Tasks.Add(projectTask);
                        db.SaveChanges();

                        return RedirectToAction("Info", "Project", new { id = projectTask.ProjectID });
                    }
                }

                return View("~/Views/Shared/Error.cshtml");
            }

            return View(projectTask);
        }
    }
}
