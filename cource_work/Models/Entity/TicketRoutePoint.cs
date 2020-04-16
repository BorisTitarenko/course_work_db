using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class TicketRoutePoint
    {
        public int TrpId { get; set; }
        public int? TicketId { get; set; }
        public int? RpId { get; set; }

        public virtual RoutePoint Rp { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
