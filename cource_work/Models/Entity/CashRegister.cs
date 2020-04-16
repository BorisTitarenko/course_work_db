using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class CashRegister
    {
        public CashRegister()
        {
            Cashier = new HashSet<Cashier>();
            DayCashAmount = new HashSet<DayCashAmount>();
        }

        public int CrId { get; set; }
        public bool? WithCard { get; set; }
        public string RegisterModel { get; set; }
        public string ComputerModel { get; set; }
        public string ComputerOs { get; set; }
        public double? ComputerPrice { get; set; }

        public virtual ICollection<Cashier> Cashier { get; set; }
        public virtual ICollection<DayCashAmount> DayCashAmount { get; set; }
    }
}
