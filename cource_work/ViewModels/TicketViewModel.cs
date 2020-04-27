using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.ViewModels
{
    public class TicketViewModel
    {
        public int TicketId { get; set; }
        public string City { get; set; }
        public int TripId { get; set; }
        public string RouteName { get; set; }
        public TimeSpan PaimentTime { get; set; }
        public DateTime DepartureDate { get; set; }
        public decimal TicketPrice { get; set; }
        public decimal Pdv { get; set; }
        public string PassengerName { get; set; }
    }
}
