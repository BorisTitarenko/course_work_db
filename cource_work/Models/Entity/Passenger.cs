using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Passenger
    {
        public Passenger()
        {
            Baggage = new HashSet<Baggage>();
            Ticket = new HashSet<Ticket>();
        }

        public int PassengerId { get; set; }
        public string PassengerName { get; set; }
        public int PassengerAge { get; set; }
        public bool? Preferential { get; set; }

        public virtual ICollection<Baggage> Baggage { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
