using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class RoutePoint
    {
        public RoutePoint()
        {
            JourneyRoutePoint = new HashSet<JourneyRoutePoint>();
            RouteRoutePoint = new HashSet<RouteRoutePoint>();
            Ticket = new HashSet<Ticket>();
        }

        public int RpId { get; set; }
        public string CityName { get; set; }
        public string StopName { get; set; }

        public virtual ICollection<JourneyRoutePoint> JourneyRoutePoint { get; set; }
        public virtual ICollection<RouteRoutePoint> RouteRoutePoint { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
