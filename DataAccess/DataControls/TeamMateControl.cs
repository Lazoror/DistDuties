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

        public TeamMate CreateMate(string email, Guid projectId, Guid userId)
        {
            TeamMate teamMate = new TeamMate() { Email = email, ProjectID = projectId, UserID = userId };

            return teamMate;
        }

        public bool DeleteTeamMate(string email, Guid projectId)
        {
            TeamMate teamMateFind = db.Projects.Where(a => a.ProjectID == projectId).SelectMany(a => a.TeamMates).Where(a => a.Email == email).SingleOrDefault();

            if (teamMateFind != null)
            {
                teamMateFind.MateStatus = TeamMateStatus.Deleted;

                db.Entry(teamMateFind).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks if teammate already exists with "Deleted" status. 
        /// If true, sets status "Active" and saving changes.
        /// If false, set status "Active" to the new teammate and saving changes.
        /// </summary>
        /// <param name="teamMate"></param>
        public void AddTeamMateSave(TeamMate teamMate)
        {
            teamMate.MateStatus = TeamMateStatus.Active;

            db.TeamMates.Add(teamMate);
            db.SaveChanges();
        }

        /// <summary>
        /// Checks if user is teammate of the project and set new status if teammate status is not equal to new status.
        /// </summary>
        /// <param name="teamMate">Teammate object</param>
        /// <param name="setStatus">enum TeamMateStatus</param>
        /// <returns>
        /// Set status and true if teammate status not equal to set status.
        /// False if user is not in project or old status and new status are equal.
        /// </returns>
        public bool UpdateMateStatus(TeamMate teamMate, TeamMateStatus setStatus)
        {
            if (IsUserTeamMate(teamMate.ProjectID, teamMate.Email))
            {
                if(teamMate.MateStatus != setStatus)
                {
                    var tm = db.TeamMates.FirstOrDefault(a => a.TeamMateID == teamMate.TeamMateID);

                    tm.MateStatus = setStatus;
                    db.SaveChanges();

                    return true;
                }
            }

            return false;
        }

        public bool IsUserTeamMate(Guid projectId, string userEmail)
        {
            bool isUserInProject = db.TeamMates.Any(a => a.ProjectID == projectId && a.Email == userEmail);

            return isUserInProject;
        }
    }
}
