using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cource_work.Models.Entity
{
    public partial class User
    {
        public User() { 
        }

        

        public virtual Employee Employee { get; set; }
        public virtual Role Role { get; set; }
    }
}
