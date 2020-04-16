using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Mechanic
    {
        public int MechanicId { get; set; }
        public int EmployerId { get; set; }
        public string Qualification { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
