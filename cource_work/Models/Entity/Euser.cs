using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Euser
    {
        public int EuserId { get; set; }
        public string EuserLogin { get; set; }
        public string EuserPassword { get; set; }
        public int EmployeeId { get; set; }
        public int RoleId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
