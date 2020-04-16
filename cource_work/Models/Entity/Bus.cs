using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Bus
    {
        public Bus()
        {
            Journey = new HashSet<Journey>();
        }

        public int BusId { get; set; }
        public string BusModel { get; set; }
        public int SitingNumber { get; set; }
        public bool? ClimateControl { get; set; }
        public int? CcId { get; set; }

        public virtual CarrierCompany Cc { get; set; }
        public virtual ICollection<Journey> Journey { get; set; }
    }
}
