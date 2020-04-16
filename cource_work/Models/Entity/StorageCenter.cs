using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class StorageCenter
    {
        public StorageCenter()
        {
            StoreBaggage = new HashSet<StoreBaggage>();
            StoreKeeper = new HashSet<StoreKeeper>();
        }

        public int SCenterId { get; set; }
        public int AccountingId { get; set; }
        public int PlaceNumber { get; set; }
        public double TotalAmount { get; set; }
        public DateTime StartPerion { get; set; }
        public DateTime EndPeriod { get; set; }

        public virtual Accounting Accounting { get; set; }
        public virtual ICollection<StoreBaggage> StoreBaggage { get; set; }
        public virtual ICollection<StoreKeeper> StoreKeeper { get; set; }
    }
}
