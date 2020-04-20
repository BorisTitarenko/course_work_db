using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class DayCashAmount
    {
        public DayCashAmount()
        {
            CashTransaction = new HashSet<CashTransaction>();
        }

        public int DcaId { get; set; }
        public double TotalDayAmount { get; set; }
        public DateTime WorkDate { get; set; }
        public int AccId { get; set; }
        public int CrId { get; set; }
        public TimeSpan LastTransactionTime { get; set; }

        public virtual Accounting Acc { get; set; }
        public virtual CashRegister Cr { get; set; }
        public virtual ICollection<CashTransaction> CashTransaction { get; set; }
    }
}
