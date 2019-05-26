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
        private DistContext db = new DistContext();

        public void AddTicketSave(Ticket ticket)
        {
            ticket.Status = TaskStatus.New;
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }
    }
}
