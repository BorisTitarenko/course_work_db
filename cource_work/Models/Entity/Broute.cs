using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Broute
    {
        public Broute()
        {
            Journey = new HashSet<Journey>();
            RouteRoutePoint = new HashSet<RouteRoutePoint>();
        }

        public int RouteId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public int? RouteLength { get; set; }

        public virtual ICollection<Journey> Journey { get; set; }
        public virtual ICollection<RouteRoutePoint> RouteRoutePoint { get; set; }
    }
}
