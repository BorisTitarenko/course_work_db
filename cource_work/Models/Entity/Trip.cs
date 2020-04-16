using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace cource_work.Models.Entity
{
    public partial class Trip
    {
        public Trip()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int TripId { get; set; }
        public int? PassangersCount { get; set; }
        public int? DispatcherId { get; set; }
        public int JourneyId { get; set; }
        public string DeportingStat { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public DateTime? DeportingDate { get; set; }

        public virtual Dispatcher Dispatcher { get; set; }
        public virtual Journey Journey { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
