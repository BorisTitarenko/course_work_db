using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Employee
    {
        public Employee()
        {
            Euser = new HashSet<Euser>();
            Salary = new HashSet<Salary>();
        }

        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeePassport { get; set; }
        public string EmployeeWorkBook { get; set; }
        public string EmployeePhone { get; set; }

        public virtual ICollection<Euser> Euser { get; set; }
        public virtual ICollection<Salary> Salary { get; set; }
    }
}
