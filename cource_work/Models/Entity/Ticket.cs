using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace cource_work.Models.Entity
{
    public partial class Ticket
    {
        public int TicketId { get; set; }
        public int Seat { get; set; }
        public int PassengerId { get; set; }
        public int CtId { get; set; }
        public int TripId { get; set; }
        public int RpId { get; set; }

        public virtual CashTransaction Ct { get; set; }
        public virtual Passenger Passenger { get; set; }
        public virtual RoutePoint Rp { get; set; }
        public virtual Trip Trip { get; set; }
    }
}
