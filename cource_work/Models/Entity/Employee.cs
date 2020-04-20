using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Employee
    {
        public Employee()
        {
            Euser = new HashSet<Euser>();
            Vacation = new HashSet<Vacation>();
        }

        public int EmployeeId { get; set; }
        public int AccountingId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePassport { get; set; }
        public string EmployeeWorkBook { get; set; }
        public string EmployerShift { get; set; }
        public double EmployeeSalary { get; set; }
        public string EmployeePhone { get; set; }

        public virtual Accounting Accounting { get; set; }
        public virtual ICollection<Euser> Euser { get; set; }
        public virtual ICollection<Vacation> Vacation { get; set; }
    }
}
