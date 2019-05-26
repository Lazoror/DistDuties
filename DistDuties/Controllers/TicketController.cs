using DataAccess.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccess.Models;
using DataAccess.DataControls;
using System;

namespace DistDuties.Controllers
{
    public class TicketController : Controller
    {
        private readonly ProjectControl projectControl = new ProjectControl();
        private readonly TicketControl ticketControl = new TicketControl();

        // GET: Task
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult Create(Guid projectId)
        {

            if (projectId != null)
            {
                if (projectControl.IsProjectExists(projectId))
                {

                    // Check if current user is admin of the project.
                    Project project = projectControl.GetProjectByAdmin(projectId, User.Identity.Name);

                    // If current user is not admin, access denied.
                    if (project == null)
                    {
                        return View("~/Views/Shared/Error.cshtml");
                    }

                    // If current user is admin, return ticket create form.
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
                // Find team mate in project with email.
                TeamMate teamMate = projectControl.GetMateProject(ticket.ProjectID, ticket.TeamMateEmail);

                if(teamMate != null)
                {
                    if (ticket.ProjectID == teamMate.ProjectID)
                    {
                        ticket.TeamMateID = teamMate.TeamMateID;
                        
                        ticketControl.AddTicketSave(ticket);

                        return RedirectToAction("Info", "Project", new { id = ticket.ProjectID });
                    }
                }

                return View("~/Views/Shared/Error.cshtml");
            }

            return View(ticket);
        }

        public ActionResult ProjectTickets()
        {
            return View();
        }
    }
}
