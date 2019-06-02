using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace DataAccess.DataControls
{
    public class TicketControl
    {
        private readonly DistContext db = new DistContext();

        public void AddTicketSave(Ticket ticket)
        {
            ticket.Status = TicketStatus.New;
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }

        public Ticket GetTicketById(Guid ticketId)
        {
            return db.Tickets.FirstOrDefault(a => a.TicketID == ticketId);
        }

        public bool UpdateStatus(Ticket ticket, TicketStatus status)
        {
            if(ticket == null)
            {
                return false;
            }
            else
            {
                ticket.Status = status;
                db.Entry(ticket).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();

                return true;
            }
            
        }
    }
}
