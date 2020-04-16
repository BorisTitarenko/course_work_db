using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Broute
    {
        public Broute()
        {
            Journey = new HashSet<Journey>();
            RoutePoint = new HashSet<RoutePoint>();
        }

        public int RouteId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? RouteLength { get; set; }

        public virtual ICollection<Journey> Journey { get; set; }
        public virtual ICollection<RoutePoint> RoutePoint { get; set; }
    }
}
