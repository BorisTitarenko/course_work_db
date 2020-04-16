using System;
using System.Collections.Generic;

namespace cource_work.Models.Entity
{
    public partial class Driver
    {
        public Driver()
        {
            Journey = new HashSet<Journey>();
        }

        public int DriverId { get; set; }
        public double? DriverSalary { get; set; }
        public DateTime DriverBirthDate { get; set; }
        public string DriverLicence { get; set; }
        public string DriverPassport { get; set; }
        public string DriverIdenCode { get; set; }
        public string DriverEmpHistory { get; set; }
        public string DriverAdress { get; set; }
        public string DriverPhone { get; set; }
        public bool LikeShanson { get; set; }
        public int? CcId { get; set; }
        public string DriverName { get; set; }

        public virtual CarrierCompany Cc { get; set; }
        public virtual ICollection<Journey> Journey { get; set; }
    }
}
