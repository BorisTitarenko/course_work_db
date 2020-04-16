using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Baggage
    {
        public Baggage()
        {
            StoreBaggage = new HashSet<StoreBaggage>();
        }

        public int BaggageId { get; set; }
        public double? BWeight { get; set; }
        public double? BVolume { get; set; }
        public bool? NeedCare { get; set; }
        public int PassengerId { get; set; }

        public virtual Passenger Passenger { get; set; }
        public virtual ICollection<StoreBaggage> StoreBaggage { get; set; }
    }
}
