using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using cource_work.Models.Entity;

namespace cource_work.ViewModels
{
    public partial class TicketRoutePointTransactionViewModel
    {
        public TicketRoutePointTransactionViewModel(){
            RoutePoints = new HashSet<RoutePoint>();
            Trips = new HashSet<Trip>();
            Passengers = new HashSet<Passenger>();
        }
        public int RPId { get; set; }
        public int TripId { get; set; }
        public Decimal NDS { get; set; }
        public TimeSpan PaimentTime { get; set; }
        public DateTime DepartureDate { get; set; }
        public int DayCashAmountId { get; set; }
        public int Seat { get; set; }
        public int PassengerId { get; set; }
        public decimal TicketPrice { get; set; }

        public virtual ICollection<Passenger> Passengers { get; set; }
        public virtual ICollection<Trip> Trips { get; set; }
        public virtual ICollection<RoutePoint> RoutePoints { get; set; }
    }
}
