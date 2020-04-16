using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.Models.Entity
{
    public class SPSearchJourneysByString
    {
        public int JourneyId { get; set; }
        public int? BdrId { get; set; }
        public int RouteId { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public int BusId { get; set; }
        public string BusModel { get; set; }
        public int DriverId { get; set; }
        public string DriverName { get; set; }
        public DateTime? DepartureTime { get; set; }
        public int? PassangersCount { get; set; }
        public int? DispatcherId { get; set; }
        public string DeportingStat { get; set; }

        public virtual Dispatcher Dispatcher { get; set; }
    }
}
