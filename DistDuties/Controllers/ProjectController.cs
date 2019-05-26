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

            ViewBag.userID = userID;

            var projects = projectControl.GetAllUserProjects(userID);


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
                project.CreatorEmail = User.Identity.Name;

                projectControl.AddProject(project);
                projectControl.SaveChanges();

                TeamMate teamMate = mateControl.CreateMate(User.Identity.Name, project.ProjectID, User.Identity.GetUserId());
                mateControl.AddTeamMateSave(teamMate);
            }

            return RedirectToAction("Index", "Project");
        }

        [Authorize]
        public ActionResult Info(int? id)
        {
            int projectId = id ?? -1;

            string userEmail = User.Identity.Name;
            bool isUserInProject = mateControl.IsUserTeamMate(projectId, userEmail);

           
            if (isUserInProject)
            {
                Project project = projectControl.FindProjectById(projectId);

                ViewBag.temMates = projectControl.GetTeamMatesEmail(projectId);

                if (project.CreatorEmail == userEmail)
                {
                    ViewBag.Tickets = projectControl.GetAllTickets(projectId);
                }
                else
                {
                    ViewBag.Tickets = projectControl.GetUserTickets(projectId, userEmail);
                }
                
                return View(project);
            }

            
            return RedirectToAction("Index", "Project");
        }


        [Authorize]
        public ActionResult UserControls(string email, string userId, int projectId, string userControl)
        {
            if (!String.IsNullOrEmpty(email))
            {

                if (userControl == "Delete")
                {
                    Project project = projectControl.FindProjectById(projectId);

                    if(project.CreatorEmail != email)
                    {
                        mateControl.DeleteTeamMate(email, projectId);
                    }

                }

                if (userControl == "Add")
                {
                    TeamMate teamMateFind = projectControl.GetMateProject(projectId, email);
                    

                    if (teamMateFind == null)
                    {
                        TeamMate teamMate = mateControl.CreateMate(email, projectId, userId);

                        mateControl.AddTeamMateSave(teamMate);

                    }
                }

            }

            return RedirectToAction("Info", new { id = projectId });
        }
    }
}
