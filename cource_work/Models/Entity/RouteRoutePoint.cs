using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class RouteRoutePoint
    {
        public int RrpId { get; set; }
        public int RouteId { get; set; }
        public int RpId { get; set; }

        public virtual Broute Route { get; set; }
        public virtual RoutePoint Rp { get; set; }
    }
}
