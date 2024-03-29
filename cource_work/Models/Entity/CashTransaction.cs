﻿using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class CashTransaction
    {
        public CashTransaction()
        {
            Ticket = new HashSet<Ticket>();
        }

        public int CtId { get; set; }
        public TimeSpan PayTime { get; set; }
        public bool? WithCart { get; set; }
        public int DcaId { get; set; }
        public decimal TotalCash { get; set; }

        public virtual DayCashAmount Dca { get; set; }
        public virtual ICollection<Ticket> Ticket { get; set; }
    }
}
