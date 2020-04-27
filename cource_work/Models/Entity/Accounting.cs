using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Accounting
    {
        public Accounting()
        {
            DayCashAmount = new HashSet<DayCashAmount>();
            Salary = new HashSet<Salary>();
            TransportationCosts = new HashSet<TransportationCosts>();
        }

        public int AccId { get; set; }
        public decimal TransportationAmount { get; set; }
        public decimal SalaryAmount { get; set; }
        public decimal Pdv { get; set; }
        public decimal TicketAmount { get; set; }
        public DateTime? StartPerion { get; set; }
        public DateTime? EndPerion { get; set; }
        public decimal? Pp { get; set; }

        public virtual ICollection<DayCashAmount> DayCashAmount { get; set; }
        public virtual ICollection<Salary> Salary { get; set; }
        public virtual ICollection<TransportationCosts> TransportationCosts { get; set; }
    }
}
