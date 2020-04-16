using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class StationPlatform
    {
        public StationPlatform()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int SpId { get; set; }
        public int SpNumber { get; set; }
        public string WorldPart { get; set; }

        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
