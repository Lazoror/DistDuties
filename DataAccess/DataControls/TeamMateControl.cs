using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataControls
{
    public class TeamMateControl
    {
        private DistContext db = new DistContext();
        
        public TeamMate CreateMate(string email, int projectId, string userId)
        {
            TeamMate teamMate = new TeamMate() { Email = email, ProjectID = projectId, UserID = userId };

            return teamMate;
        }

        public bool DeleteTeamMate(string email, int projectId)
        {
            TeamMate teamMateFind = db.Projects.Where(a => a.ProjectID == projectId).SelectMany(a => a.TeamMates).Where(a => a.Email == email).SingleOrDefault();

            if (teamMateFind != null)
            {
                db.TeamMates.Remove(teamMateFind);
                db.SaveChanges();

                return true;
            }

            return false;
        }

        public void AddTeamMateSave(TeamMate teamMate)
        {
            db.TeamMates.Add(teamMate);
            db.SaveChanges();
        }

        public bool IsUserTeamMate(int? projectId, string userEmail)
        {
            projectId = projectId ?? -1;

            bool isUserInProject = db.TeamMates.Any(a => a.ProjectID == projectId && a.Email == userEmail);

            return isUserInProject;
        }
    }
}
