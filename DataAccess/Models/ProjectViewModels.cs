using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace DataAccess.Models
{
    public enum TicketStatus
    {
        New,
        InProgress,
        Completed,
        Closed

    }

    public enum TeamMateStatus
    {
        Active,
        UnActive,
        Deleted
    }

    public enum ProjectStatus
    {
        Active,
        Stopped,
        Closed
    }

    public class Project
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid ProjectID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatorEmail { get; set; }
        public ProjectStatus ProjectStatus { get; set; }

        public virtual ICollection<TeamMate> TeamMates { get; set; }
    }

    public class TeamMate
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TeamMateID { get; set; }
        public Guid UserID { get; set; }
        public Guid ProjectID { get; set; }
        public string Email { get; set; }
        public TeamMateStatus MateStatus { get; set; }

        public virtual Project Project { get; set; }
        public virtual ICollection<Ticket> Tasks { get; set; }
    }

    public class Ticket
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid TicketID { get; set; }
        [StringLength(60)]
        [Required]
        public string TicketName { get; set; }
        public Guid ProjectID { get; set; }
        public Guid TeamMateID { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        public string TeamMateEmail { get; set; }
        public TicketStatus Status { get; set; }
        [DataType(DataType.Date)]
        public DateTime DeadLine { get; set; }

        public virtual TeamMate TeamMate { get; set; }

    }
}