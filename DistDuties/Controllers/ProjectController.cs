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
using Microsoft.AspNet.Identity;

namespace DistDuties.Controllers
{
    public class ProjectController : Controller
    {
        private DistContext db;

        public ProjectController()
        {
            db = new DistContext();
        }

        [Authorize]
        public ActionResult Index()
        {
            string userID = User.Identity.GetUserId();

            ViewBag.userID = userID;

            var projects = db.Projects.Where(a => a.TeamMates.Any(mate => mate.UserID == userID)).ToList();


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
                TeamMate teamMate = new TeamMate();

                project.CreatorEmail = User.Identity.Name;

                db.Projects.Add(project);
                db.SaveChanges();

                teamMate.ProjectID = project.ProjectID;
                teamMate.UserID = User.Identity.GetUserId();
                teamMate.Email = User.Identity.Name;
                db.TeamMates.Add(teamMate);
                db.SaveChanges();

            }

            return RedirectToAction("Index", "Project");
        }

        [Authorize]
        public ActionResult Info(int? id)
        {
            bool isUserInProject = db.TeamMates.Any(a => a.ProjectID == id && a.Email == User.Identity.Name);

            if (isUserInProject)
            {
                var project = db.Projects.Find(id);

                ViewBag.temMates = db.TeamMates.Where(a => a.ProjectID == id).Select(a => a.Email).ToList();
                ViewBag.projectTasks = db.TeamMates.SelectMany(a => a.Tasks).Where(a => a.ProjectID == id).ToList();


                return View(project);
            }

            

            return RedirectToAction("Index", "Project");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        [Authorize]
        public ActionResult UserControls(string email, string userId, int projectId, string userControl)
        {
            if (!String.IsNullOrEmpty(email))
            {
                

                if (userControl == "Delete")
                {
                    Project project = db.Projects.FirstOrDefault(a => a.ProjectID == projectId);

                    if(project.CreatorEmail != email)
                    {
                        DeleteTeamMate(email, projectId);
                    }

                }

                if (userControl == "Add")
                {
                    TeamMate teamMateFind = db.TeamMates.FirstOrDefault(a => a.ProjectID == projectId && a.UserID == userId);

                    if (teamMateFind == null)
                    {
                        TeamMate teamMate = new TeamMate();

                        teamMate.Email = email;
                        teamMate.ProjectID = projectId;
                        teamMate.UserID = userId;

                        db.TeamMates.Add(teamMate);
                        db.SaveChanges();
                    }
                }

            }

            return RedirectToAction("Info", new { id = projectId });
        }

        private void DeleteTeamMate(string email, int projectId)
        {
            TeamMate teamMateFind = db.TeamMates.FirstOrDefault(a => a.ProjectID == projectId && a.Email == email);

            if (teamMateFind != null)
            {
                db.TeamMates.Remove(teamMateFind);
                db.SaveChanges();
            }
        }
    }
}
