using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Ticket
    {
        public Ticket()
        {
            TicketRoutePoint = new HashSet<TicketRoutePoint>();
        }

        public int TicketId { get; set; }
        public double Nds { get; set; }
        public int? Seat { get; set; }
        public int PassengerId { get; set; }
        public int CtId { get; set; }
        public int? TripId { get; set; }

        public virtual CashTransaction Ct { get; set; }
        public virtual Passenger Passenger { get; set; }
        public virtual Trip Trip { get; set; }
        public virtual ICollection<TicketRoutePoint> TicketRoutePoint { get; set; }
    }
}
