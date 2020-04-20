using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Employer
    {
        public Employer()
        {
           
            Vacation = new HashSet<Vacation>();
        }

        public int EmployerId { get; set; }
        public int AccountingId { get; set; }
        public string EmployerName { get; set; }
        public string EmployerPassport { get; set; }
        public string EmployerWorkBook { get; set; }
        public string EmployerShift { get; set; }
        public double EmployerSalary { get; set; }
        public string EmployerPhone { get; set; }

        public virtual Accounting Accounting { get; set; }
       
        public virtual ICollection<Vacation> Vacation { get; set; }
    }
}
