using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class JourneyRoutePoint
    {
        public int TrpId { get; set; }
        public int JourneyId { get; set; }
        public int RpId { get; set; }
        public decimal? TicketPrice { get; set; }
        public TimeSpan? ArrivalTime { get; set; }

        public virtual Journey Journey { get; set; }
        public virtual RoutePoint Rp { get; set; }
    }
}
