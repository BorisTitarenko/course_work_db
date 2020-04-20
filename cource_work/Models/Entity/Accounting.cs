using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Accounting
    {
        public Accounting()
        {
            DayCashAmount = new HashSet<DayCashAmount>();
            Employer = new HashSet<Employer>();
            TransportationCosts = new HashSet<TransportationCosts>();
        }

        public int AccId { get; set; }
        public double TransportationAmount { get; set; }
        public double SalaryAmount { get; set; }
        public double ServiceAmount { get; set; }
        public double Nds { get; set; }
        public double InsuranceAmount { get; set; }
        public double TicketAmount { get; set; }
        public DateTime? StartPerion { get; set; }
        public DateTime? EndPerion { get; set; }

        public virtual ICollection<DayCashAmount> DayCashAmount { get; set; }
        public virtual ICollection<Employer> Employer { get; set; }
        public virtual ICollection<TransportationCosts> TransportationCosts { get; set; }
    }
}
