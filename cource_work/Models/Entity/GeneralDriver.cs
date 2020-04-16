using System;
using System.Collections.Generic;

namespace cource_work.Models
{
    public partial class GeneralDriver
    {
        public string CcName { get; set; }
        public string OfficeAdress { get; set; }
        public string CcOwner { get; set; }
        public string CcPhone { get; set; }
        public double? DriverSalary { get; set; }
        public string DriverName { get; set; }
        public DateTime? DriverBirthDate { get; set; }
        public string DriverLicence { get; set; }
        public string DriverPassport { get; set; }
        public string DriverIdenCode { get; set; }
        public string DriverEmpHistory { get; set; }
        public string DriverAdress { get; set; }
        public string DriverPhone { get; set; }
        public bool? LikeShanson { get; set; }
        public string DriverRole { get; set; }
        public string BusModel { get; set; }
        public int? SitingNumber { get; set; }
        public int? StandingNumber { get; set; }
        public int? DisablePeopleNumber { get; set; }
        public bool? ClimateControl { get; set; }
        public string StartPoint { get; set; }
        public string EndPoint { get; set; }
        public TimeSpan? StartTime { get; set; }
        public TimeSpan? EndTime { get; set; }
        public int? RouteLength { get; set; }
    }
}
