using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.ViewModels
{
    public class RoutePointViewModel
    {
        public int RpId { get; set; }
        public int RouteId { get; set; }
        public int JourneyId { get; set; }
        public string CityName { get; set; }
        public string? StopName { get; set; }
        public decimal TicketPrice { get; set; }
        public TimeSpan ArrivalTime { get; set; }
    }
}
