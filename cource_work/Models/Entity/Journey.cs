using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Journey
    {
        public Journey()
        {
            JourneyRoutePoint = new HashSet<JourneyRoutePoint>();
            TransportationCosts = new HashSet<TransportationCosts>();
            Trip = new HashSet<Trip>();
        }

        public int JourneyId { get; set; }
        public int DriverId { get; set; }
        public int BusId { get; set; }
        public int RouteId { get; set; }
        public TimeSpan? DeportingTime { get; set; }
        public int? AffectDistance { get; set; }
        public decimal? CarrierCompanyCosts { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }

        public virtual Bus Bus { get; set; }
        public virtual Driver Driver { get; set; }
        public virtual Broute Route { get; set; }
        public virtual ICollection<JourneyRoutePoint> JourneyRoutePoint { get; set; }
        public virtual ICollection<TransportationCosts> TransportationCosts { get; set; }
        public virtual ICollection<Trip> Trip { get; set; }
    }
}
