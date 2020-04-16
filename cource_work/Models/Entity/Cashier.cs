using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Cashier
    {
        public int CashierId { get; set; }
        public int EmployerId { get; set; }
        public string Religion { get; set; }
        public int? CrId { get; set; }

        public virtual CashRegister Cr { get; set; }
        public virtual Employer Employer { get; set; }
    }
}
