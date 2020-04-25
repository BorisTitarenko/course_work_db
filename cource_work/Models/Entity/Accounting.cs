using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        public decimal PDV { get; set; }
        public decimal? PP { get; set; }
        public decimal TicketAmount { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? StartPerion { get; set; }

        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? EndPerion { get; set; }

        public virtual ICollection<DayCashAmount> DayCashAmount { get; set; }
        public virtual ICollection<Salary> Salary { get; set; }
        public virtual ICollection<TransportationCosts> TransportationCosts { get; set; }
    }
}
