using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class CarrierCompany
    {
        public CarrierCompany()
        {
            Bus = new HashSet<Bus>();
            Driver = new HashSet<Driver>();
        }

        public int CcId { get; set; }
        public string CcName { get; set; }
        public string OfficeAdress { get; set; }
        public string CcOwner { get; set; }
        public string CcPhone { get; set; }

        public virtual ICollection<Bus> Bus { get; set; }
        public virtual ICollection<Driver> Driver { get; set; }
    }
}
