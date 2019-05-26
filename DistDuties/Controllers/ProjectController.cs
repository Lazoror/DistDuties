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

    [RoutePrefix("projects")]
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

            Guid userId;

            Guid.TryParse(userID, out userId);

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
                Guid userId;
                Guid.TryParse(User.Identity.GetUserId(), out userId);

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


            if (isUserInProject)
            {
                Project project = projectControl.FindProjectById(id);

                ViewBag.temMates = projectControl.GetMatesActiveEmail(id);

                if (project.CreatorEmail == userEmail)
                {
                    ViewBag.Tickets = projectControl.GetAllTickets(id);
                }
                else
                {
                    ViewBag.Tickets = projectControl.GetUserTickets(id, userEmail);
                }

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
                    Project project = projectControl.FindProjectById(projectId);

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
                        Guid userIdGuid;
                        Guid.TryParse(userId, out userIdGuid);

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
