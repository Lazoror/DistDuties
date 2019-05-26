using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataControls
{
    public class ProjectControl
    {
        private DistContext db = new DistContext();

        public IEnumerable<Project> GetAllUserProjects(Guid userId)
        {
            var projects = db.Projects.Where(a => a.TeamMates.Any(b => b.UserID == userId));

            return projects;
        }

        public void AddProject(Project project)
        {
            project.ProjectStatus = ProjectStatus.Active;
            db.Projects.Add(project);
        }


        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public Project FindProjectById(Guid projectId)
        {
            Project project = db.Projects.FirstOrDefault(a => a.ProjectID == projectId);

            return project;
        }

        public IQueryable<string> GetMatesActiveEmail(Guid projectId)
        {
            var teamMates = db.Projects.Where(a => a.ProjectID == projectId).Include(a => a.TeamMates).SelectMany(a => a.TeamMates).Where(a => a.MateStatus == TeamMateStatus.Active).Select(a => a.Email);

            return teamMates;
        }

        public IEnumerable<Ticket> GetAllTickets(Guid projectId)
        {
            var tickets = db.Projects.Where(a => a.ProjectID == projectId).Include(a => a.TeamMates).SelectMany(a => a.TeamMates).Include(a => a.Tasks).SelectMany(a => a.Tasks);

            return tickets;
        }

        public IEnumerable<Ticket> GetUserTickets(Guid projectId, string userEmail)
        {
            var tickets = db.Projects.Where(a => a.ProjectID == projectId).Include(a => a.TeamMates).SelectMany(a => a.TeamMates).Include(a => a.Tasks).SelectMany(a => a.Tasks).Where(a => a.TeamMateEmail == userEmail);

            return tickets;
        }

        /// <summary>
        /// Gets the user object from project.
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="userEmail"></param>
        /// <returns>The TeamMate object if such user is in the project.
        /// The null if there is no such user.
        /// </returns>
        public TeamMate GetMateProject(Guid projectId, string userEmail)
        {
            TeamMate teamMateFind = db.Projects.Include(a => a.TeamMates).SelectMany(a => a.TeamMates).FirstOrDefault(a => a.ProjectID == projectId && a.Email == userEmail);

            return teamMateFind;
        }

        public bool IsProjectExists(Guid projectId)
        {
            bool isProjectExists = db.Projects.Any(a => a.ProjectID == projectId);

            return isProjectExists;
        }

        public Project GetProjectByAdmin(Guid projectId, string adminEmail)
        {
            Project project = db.Projects.FirstOrDefault(a => a.ProjectID == projectId && a.CreatorEmail == adminEmail);

            return project;
        }

    }
}
