using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Employee
    {
        public Employee()
        {
           
            Vacation = new HashSet<Vacation>();
        }

        public int EmployeeId { get; set; }
        public int AccountingId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePassport { get; set; }
        public string EmployeeWorkBook { get; set; }
        public string EmployeeShift { get; set; }
        public double EmployeeSalary { get; set; }
        public string EmployeePhone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string EmployeeLogin { get; set; }

        public virtual Accounting Accounting { get; set; }
       
        public virtual ICollection<Vacation> Vacation { get; set; }
    }
}
