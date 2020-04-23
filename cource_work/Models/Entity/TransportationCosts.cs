using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class TransportationCosts
    {
        public int CsId { get; set; }
        public int AccountingId { get; set; }
        public int JourneyId { get; set; }
        public decimal CarrierComCost { get; set; }
        public decimal FuelCosts { get; set; }
        public decimal Inssurance { get; set; }

        public virtual Accounting Accounting { get; set; }
        public virtual Journey Journey { get; set; }
    }
}
