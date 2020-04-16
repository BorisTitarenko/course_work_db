using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class StoreBaggage
    {
        public int SbId { get; set; }
        public int? SCenterId { get; set; }
        public int? BaggageId { get; set; }
        public int? SbWeight { get; set; }
        public double? SbVolume { get; set; }
        public int SbNumber { get; set; }
        public int SbHours { get; set; }
        public double TotalAmount { get; set; }

        public virtual Baggage Baggage { get; set; }
        public virtual StorageCenter SCenter { get; set; }
    }
}
