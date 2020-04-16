using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class StoreKeeper
    {
        public int SKeeperId { get; set; }
        public int EmployerId { get; set; }
        public int SCenterId { get; set; }

        public virtual Employer Employer { get; set; }
        public virtual StorageCenter SCenter { get; set; }
    }
}
