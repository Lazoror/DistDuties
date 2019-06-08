using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataAccess.DAL;
using DataAccess.DataControls;
using DataAccess.Models;
using Microsoft.AspNet.Identity;

namespace DistDuties.Controllers
{

    public class ProjectController : Controller
    {
        private readonly ProjectControl projectControl = new ProjectControl();
        private readonly TeamMateControl mateControl = new TeamMateControl();

        public ProjectController()
        {

        }

        [Authorize]
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();


            Guid.TryParse(userID, out Guid userId);

            ViewBag.userID = userID;

            var projects = projectControl.GetAllUserProjects(userId);


            return View(projects);
        }

        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(Project project)
        {
            if (ModelState.IsValid)
            {
                Guid.TryParse(User.Identity.GetUserId(), out Guid userId);

                project.CreatorEmail = User.Identity.Name;

                projectControl.AddProject(project);
                projectControl.SaveChanges();

                TeamMate teamMate = mateControl.CreateMate(User.Identity.Name, project.ProjectID, userId);
                mateControl.AddTeamMateSave(teamMate);
            }

            return RedirectToAction("Index", "Project");
        }

        // Gets project id.
        [Authorize]
        public ActionResult Info(Guid id)
        {
            
            string userEmail = User.Identity.Name;
            bool isUserInProject = mateControl.IsUserTeamMate(id, userEmail);
            IEnumerable<Ticket> tickets = null;

            if (isUserInProject)
            {
                Project project = projectControl.GetProjectById(id);

                ViewBag.temMates = projectControl.GetMatesActiveEmail(id);

                if (project.CreatorEmail == userEmail)
                {
                    tickets = projectControl.GetAllTickets(id);
                }
                else
                {
                    tickets = projectControl.GetUserTickets(id, userEmail);
                }

                ViewBag.NewTickets = tickets.Where(a => a.Status == TicketStatus.New).ToList();
                ViewBag.InProgressTickets = tickets.Where(a => a.Status == TicketStatus.InProgress).ToList();
                ViewBag.CompletedTickets = tickets.Where(a => a.Status == TicketStatus.Completed).ToList();
                ViewBag.ClosedTickets = tickets.Where(a => a.Status == TicketStatus.Closed).ToList();

                return View(project);
            }


            return RedirectToAction("Index", "Project");
        }


        [Authorize]
        public ActionResult UserControls(string email, string userId, Guid projectId, string userControl)
        {
            if (!String.IsNullOrEmpty(email))
            {

                if (userControl == "Delete")
                {
                    Project project = projectControl.GetProjectById(projectId);

                    if (project.CreatorEmail != email)
                    {
                        mateControl.DeleteTeamMate(email, projectId);
                    }

                }

                if (userControl == "Add")
                {
                    TeamMate teamMateFind = projectControl.GetMateProject(projectId, email);

                    if (teamMateFind == null)
                    {
                        Guid.TryParse(userId, out Guid userIdGuid);

                        TeamMate teamMate = mateControl.CreateMate(email, projectId, userIdGuid);
                        
                        mateControl.AddTeamMateSave(teamMate);

                    }
                    else if (teamMateFind.MateStatus == TeamMateStatus.Deleted)
                    {
                        mateControl.UpdateMateStatus(teamMateFind, TeamMateStatus.Active);
                    }
                }

            }

            return RedirectToAction("Info", new { id = projectId });
        }

    }
}
