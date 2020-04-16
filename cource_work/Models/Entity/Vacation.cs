using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Vacation
    {
        public int VacationId { get; set; }
        public DateTime? LeaveDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public decimal? PayLeave { get; set; }
        public int? EmployerId { get; set; }

        public virtual Employer Employer { get; set; }
    }
}
