using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Dispatcher
    {
        public Dispatcher()
        {
            Trip = new HashSet<Trip>();
        }

        public int DispatcherId { get; set; }
        public int? EmployerId { get; set; }

        public virtual Employer Employer { get; set; }
        public virtual ICollection<Trip> Trip { get; set; }
    }
}
