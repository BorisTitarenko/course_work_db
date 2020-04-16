using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class TransportationCosts
    {
        public int CsId { get; set; }
        public int AccountingId { get; set; }
        public int JourneyId { get; set; }
        public double CarrierComCost { get; set; }
        public double FuelCosts { get; set; }
        public double MechanicCosts { get; set; }
        public double Taxes { get; set; }

        public virtual Accounting Accounting { get; set; }
        public virtual Journey Journey { get; set; }
    }
}
