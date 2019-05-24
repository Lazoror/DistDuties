using DataAccess.DAL;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DataControls
{
    public class TicketControl
    {
        private DistContext db = new DistContext();

        public void AddTicketSave(Ticket ticket)
        {
            db.Tickets.Add(ticket);
            db.SaveChanges();
        }
    }
}
