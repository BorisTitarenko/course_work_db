using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class RoutePoint
    {
        public RoutePoint()
        {
            TicketRoutePoint = new HashSet<TicketRoutePoint>();
        }

        public int RpId { get; set; }
        public string CityName { get; set; }
        public string StopName { get; set; }
        public TimeSpan ArrivalTime { get; set; }
        public int? RouteId { get; set; }
        public double TicketPrice { get; set; }

        public virtual Broute Route { get; set; }
        public virtual ICollection<TicketRoutePoint> TicketRoutePoint { get; set; }
    }
}
