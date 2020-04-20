using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Role
    {
        public Role()
        {
            Euser = new HashSet<Euser>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<Euser> Euser { get; set; }
    }
}
