using DataAccess.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using DataAccess.Models;
using DataAccess.DataControls;
using System;
using System.Data;
using System.Text;

namespace DistDuties.Controllers
{
    public class TicketController : Controller
    {
        private readonly ProjectControl projectControl = new ProjectControl();
        private readonly TicketControl ticketControl = new TicketControl();

        // GET: Ticket
        [Authorize]
        public ActionResult Index(Guid ticketId)
        {
            Ticket ticket = ticketControl.GetTicketById(ticketId);

            if(ticket == null)
            {
                return View("~/Views/Shared/Error.cshtml");
            }

            Project project = projectControl.GetProjectById(ticket.ProjectID);
            string userId = User.Identity.Name;

            
            
            StringBuilder dateExpRes = new StringBuilder("");

            // Take DateTime from 'FLE Standard Time' timezone
            DateTime timeUtc = DateTime.UtcNow;
            TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");
            DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(timeUtc, cstZone);

            // Calculate how many time left to the deadline
            TimeSpan expDate = ticket.DeadLine.AddDays(1).Subtract(cstTime);

            dateExpRes.Append($"{expDate:%d} day(s) {expDate:%h} hour(s) {expDate:%m} minute(s)");


            ViewBag.timeLeft = $"Deadline - {dateExpRes}";
            ViewBag.admin = project.CreatorEmail == User.Identity.Name;

            if (ticket.TeamMateEmail == userId || project.CreatorEmail == userId)
            {
                return View(ticket);
            }

            return View("~/Views/Shared/Error.cshtml");
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
        [Authorize]
        public ActionResult Create(Ticket ticket)
        {
            if(ModelState.IsValidField(nameof(ticket.DeadLine)) && DateTime.Now.Date > ticket.DeadLine)
            {
                ModelState.AddModelError(nameof(ticket.DeadLine), "Date cannot be earlier than today");
            }

            if(ModelState.IsValidField(nameof(ticket.DeadLine)) && ticket.DeadLine > DateTime.Now.AddMonths(1))
            {
                ModelState.AddModelError(nameof(ticket.DeadLine), "Date must be less than one month");
            }

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

        public ActionResult CloseConfirm(Guid ticketId)
        {
            ViewBag.ticketId = ticketId;

            return View();
        }

        [HttpPost]
        public ActionResult CloseConfirm(Guid ticketId, string confirm)
        {
            Ticket ticket = ticketControl.GetTicketById(ticketId);

            if (ticket != null)
            {
                if (confirm == "Yes")
                {
                    ticketControl.UpdateStatus(ticket, TicketStatus.Completed);

                    return RedirectToAction("Info", "Project", new { id = ticket.ProjectID });
                }
                else
                {
                    return RedirectToAction("Index", "Ticket", new { ticketId });
                }
            }

            return View("~/Views/Shared/Error.cshtml");

        }
        
    }
}
