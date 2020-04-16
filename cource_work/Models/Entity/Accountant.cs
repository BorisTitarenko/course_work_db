using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Accountant
    {
        public int AccountantId { get; set; }
        public int EmployerId { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
