using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Salary
    {
        public int SalaryId { get; set; }
        public int EmployeeId { get; set; }
        public int AccId { get; set; }
        public decimal? EmployeeSalary { get; set; }

        public virtual Accounting Acc { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
