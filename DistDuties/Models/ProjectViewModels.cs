using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DistDuties.Models
{
    public enum TaskStatus
    {
        New,
        InProgress,
        Completed,
        Closed

    }

    public class Project
    {
        public int ProjectID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatorEmail { get; set; }

        public virtual ICollection<TeamMate> TeamMates { get; set; }
    }

    public class TeamMate
    {
        [Key]
        public int TeamMateID { get; set; }
        public string UserID { get; set; }
        public int ProjectID { get; set; }
        public int TaskID { get; set; }
        public string Email { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<ProjectTask> Tasks { get; set; }
    }

    public class ProjectTask
    {
        [Key]
        public int TaskID { get; set; }

        [StringLength(60)]
        [Required]
        public string TaskName { get; set; }
        public int ProjectID { get; set; }
        public int TeamMateID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string TeamMateEmail { get; set; }
        public TaskStatus Status { get; set; }

        public virtual TeamMate TeamMate { get; set; }

    }
}