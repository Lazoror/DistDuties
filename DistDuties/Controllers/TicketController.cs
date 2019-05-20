using DistDuties.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DistDuties.Models;

namespace DistDuties.Controllers
{
    public class TicketController : Controller
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
        public ActionResult Create(Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                TeamMate teamMate = db.TeamMates.FirstOrDefault(a => a.Email == ticket.TeamMateEmail && a.ProjectID == ticket.ProjectID);

                if(teamMate != null)
                {
                    if (ticket.ProjectID == teamMate.ProjectID)
                    {
                        ticket.TeamMateID = teamMate.TeamMateID;
                        ticket.Status = TaskStatus.New;
                        db.Tickets.Add(ticket);
                        db.SaveChanges();

                        return RedirectToAction("Info", "Project", new { id = ticket.ProjectID });
                    }
                }

                return View("~/Views/Shared/Error.cshtml");
            }

            return View(ticket);
        }
    }
}
