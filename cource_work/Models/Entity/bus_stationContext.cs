using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cource_work.Models.Entity
{
    public partial class bus_stationContext : DbContext
    {
        public bus_stationContext()
        {
        }

        public bus_stationContext(DbContextOptions<bus_stationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accountant> Accountant { get; set; }
        public virtual DbSet<Accounting> Accounting { get; set; }
        public virtual DbSet<Baggage> Baggage { get; set; }
        public virtual DbSet<Broute> Broute { get; set; }
        public virtual DbSet<Bus> Bus { get; set; }
        public virtual DbSet<CarrierCompany> CarrierCompany { get; set; }
        public virtual DbSet<CashRegister> CashRegister { get; set; }
        public virtual DbSet<CashTransaction> CashTransaction { get; set; }
        public virtual DbSet<Cashier> Cashier { get; set; }
        public virtual DbSet<DayCashAmount> DayCashAmount { get; set; }
        public virtual DbSet<Dispatcher> Dispatcher { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Employer> Employer { get; set; }
        public virtual DbSet<Journey> Journey { get; set; }
        public virtual DbSet<JourneyRoutePoint> JourneyRoutePoint { get; set; }
        public virtual DbSet<Mechanic> Mechanic { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<RoutePoint> RoutePoint { get; set; }
        public virtual DbSet<RouteRoutePoint> RouteRoutePoint { get; set; }
        public virtual DbSet<StationPlatform> StationPlatform { get; set; }
        public virtual DbSet<StorageCenter> StorageCenter { get; set; }
        public virtual DbSet<StoreBaggage> StoreBaggage { get; set; }
        public virtual DbSet<StoreKeeper> StoreKeeper { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TransportationCosts> TransportationCosts { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }
        public virtual DbSet<Vacation> Vacation { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-AHJJ5GR\\MYDB;Initial catalog=bus_station;Integrated security=False;User Id=sa;Password=password;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accountant>(entity =>
            {
                entity.Property(e => e.AccountantId).HasColumnName("accountant_id");

                entity.Property(e => e.EmployerId).HasColumnName("employer_id");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Accountant)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK__Accountan__emplo__4BAC3F29");
            });

            modelBuilder.Entity<Accounting>(entity =>
            {
                entity.HasKey(e => e.AccId)
                    .HasName("PK__Accounti__9A20D554440116CC");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.EndPerion)
                    .HasColumnName("end_perion")
                    .HasColumnType("date");

                entity.Property(e => e.InsuranceAmount).HasColumnName("insurance_amount");

                entity.Property(e => e.Nds).HasColumnName("nds");

                entity.Property(e => e.SalaryAmount).HasColumnName("salary_amount");

                entity.Property(e => e.ServiceAmount).HasColumnName("service_amount");

                entity.Property(e => e.StartPerion)
                    .HasColumnName("start_perion")
                    .HasColumnType("date");

                entity.Property(e => e.TicketAmount).HasColumnName("ticket_amount");

                entity.Property(e => e.TransportationAmount).HasColumnName("transportation_amount");
            });

            modelBuilder.Entity<Baggage>(entity =>
            {
                entity.Property(e => e.BaggageId).HasColumnName("baggage_id");

                entity.Property(e => e.BVolume).HasColumnName("b_volume");

                entity.Property(e => e.BWeight).HasColumnName("b_weight");

                entity.Property(e => e.NeedCare).HasColumnName("need_care");

                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Baggage)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK__Baggage__passeng__59FA5E80");
            });

            modelBuilder.Entity<Broute>(entity =>
            {
                entity.HasKey(e => e.RouteId)
                    .HasName("PK__BRoute__28F706FE23D5C88C");

                entity.ToTable("BRoute");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.EndPoint)
                    .IsRequired()
                    .HasColumnName("end_point")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.RouteLength).HasColumnName("route_length");

                entity.Property(e => e.StartPoint)
                    .IsRequired()
                    .HasColumnName("start_point")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.Property(e => e.BusId).HasColumnName("bus_id");

                entity.Property(e => e.BusModel)
                    .IsRequired()
                    .HasColumnName("bus_model")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.CcId).HasColumnName("cc_id");

                entity.Property(e => e.ClimateControl).HasColumnName("climate_control");

                entity.Property(e => e.SitingNumber).HasColumnName("siting_number");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.Bus)
                    .HasForeignKey(d => d.CcId)
                    .HasConstraintName("FK__Bus__cc_id__398D8EEE");
            });

            modelBuilder.Entity<CarrierCompany>(entity =>
            {
                entity.HasKey(e => e.CcId)
                    .HasName("PK__CarrierC__9F1E187BE138E9CD");

                entity.Property(e => e.CcId).HasColumnName("cc_id");

                entity.Property(e => e.CcName)
                    .IsRequired()
                    .HasColumnName("cc_name")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CcOwner)
                    .HasColumnName("cc_owner")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.CcPhone)
                    .IsRequired()
                    .HasColumnName("cc_phone")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.OfficeAdress)
                    .HasColumnName("office_adress")
                    .HasMaxLength(40)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<CashRegister>(entity =>
            {
                entity.HasKey(e => e.CrId)
                    .HasName("PK__CashRegi__AB69D8CFFDE6A1C7");

                entity.Property(e => e.CrId).HasColumnName("cr_id");

                entity.Property(e => e.ComputerModel)
                    .IsRequired()
                    .HasColumnName("computer_model")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.ComputerOs)
                    .IsRequired()
                    .HasColumnName("computer_os")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ComputerPrice).HasColumnName("computer_price");

                entity.Property(e => e.RegisterModel)
                    .IsRequired()
                    .HasColumnName("register_model")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.WithCard).HasColumnName("with_card");
            });

            modelBuilder.Entity<CashTransaction>(entity =>
            {
                entity.HasKey(e => e.CtId)
                    .HasName("PK__CashTran__33D47D0948A22B36");

                entity.Property(e => e.CtId).HasColumnName("ct_id");

                entity.Property(e => e.DcaId).HasColumnName("dca_id");

                entity.Property(e => e.PayTime).HasColumnName("pay_time");

                entity.Property(e => e.TotalCash).HasColumnName("total_cash");

                entity.Property(e => e.WithCart).HasColumnName("with_cart");

                entity.HasOne(d => d.Dca)
                    .WithMany(p => p.CashTransaction)
                    .HasForeignKey(d => d.DcaId)
                    .HasConstraintName("FK__CashTrans__dca_i__66603565");
            });

            modelBuilder.Entity<Cashier>(entity =>
            {
                entity.Property(e => e.CashierId).HasColumnName("cashier_id");

                entity.Property(e => e.CrId).HasColumnName("cr_id");

                entity.Property(e => e.EmployerId).HasColumnName("employer_id");

                entity.Property(e => e.Religion)
                    .HasColumnName("religion")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Cr)
                    .WithMany(p => p.Cashier)
                    .HasForeignKey(d => d.CrId)
                    .HasConstraintName("FK__Cashier__cr_id__5FB337D6");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Cashier)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK__Cashier__employe__5EBF139D");
            });

            modelBuilder.Entity<DayCashAmount>(entity =>
            {
                entity.HasKey(e => e.DcaId)
                    .HasName("PK__DayCashA__3726447BE1228FDD");

                entity.Property(e => e.DcaId).HasColumnName("dca_id");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.CrId).HasColumnName("cr_id");

                entity.Property(e => e.LastTransactionTime).HasColumnName("last_transaction_time");

                entity.Property(e => e.TotalDayAmount).HasColumnName("total_day_amount");

                entity.Property(e => e.WorkDate)
                    .HasColumnName("work_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.DayCashAmount)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DayCashAm__acc_i__628FA481");

                entity.HasOne(d => d.Cr)
                    .WithMany(p => p.DayCashAmount)
                    .HasForeignKey(d => d.CrId)
                    .HasConstraintName("FK__DayCashAm__cr_id__6383C8BA");
            });

            modelBuilder.Entity<Dispatcher>(entity =>
            {
                entity.Property(e => e.DispatcherId).HasColumnName("dispatcher_id");

                entity.Property(e => e.EmployerId).HasColumnName("employer_id");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Dispatcher)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Dispatche__emplo__7C4F7684");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.CcId).HasColumnName("cc_id");

                entity.Property(e => e.DriverAdress)
                    .IsRequired()
                    .HasColumnName("driver_adress")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DriverBirthDate)
                    .HasColumnName("driver_birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.DriverEmpHistory)
                    .IsRequired()
                    .HasColumnName("driver_emp_history")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DriverIdenCode)
                    .IsRequired()
                    .HasColumnName("driver_iden_code")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DriverLicence)
                    .IsRequired()
                    .HasColumnName("driver_licence")
                    .HasMaxLength(40)
                    .IsUnicode(false);

                entity.Property(e => e.DriverName)
                    .HasColumnName("driver_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.DriverPassport)
                    .IsRequired()
                    .HasColumnName("driver_passport")
                    .HasMaxLength(90)
                    .IsUnicode(false);

                entity.Property(e => e.DriverPhone)
                    .IsRequired()
                    .HasColumnName("driver_phone")
                    .HasMaxLength(14)
                    .IsUnicode(false);

                entity.Property(e => e.DriverSalary).HasColumnName("driver_salary");

                entity.Property(e => e.LikeShanson).HasColumnName("like_shanson");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.Driver)
                    .HasForeignKey(d => d.CcId)
                    .HasConstraintName("FK__Driver__cc_id__3C69FB99");
            });

            modelBuilder.Entity<Employer>(entity =>
            {
                entity.Property(e => e.EmployerId).HasColumnName("employer_id");

                entity.Property(e => e.AccountingId).HasColumnName("accounting_id");

                entity.Property(e => e.EmployerName)
                    .IsRequired()
                    .HasColumnName("employer_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerPassport)
                    .HasColumnName("employer_passport")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerPhone)
                    .IsRequired()
                    .HasColumnName("employer_phone")
                    .HasMaxLength(13)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerSalary).HasColumnName("employer_salary");

                entity.Property(e => e.EmployerShift)
                    .HasColumnName("employer_shift")
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.EmployerWorkBook)
                    .HasColumnName("employer_work_book")
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.HasOne(d => d.Accounting)
                    .WithMany(p => p.Employer)
                    .HasForeignKey(d => d.AccountingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Employer__accoun__47DBAE45");
            });

            modelBuilder.Entity<Journey>(entity =>
            {
                entity.Property(e => e.JourneyId).HasColumnName("journey_id");

                entity.Property(e => e.AffectDistance).HasColumnName("affect_distance");

                entity.Property(e => e.BusId).HasColumnName("bus_id");

                entity.Property(e => e.CarrierCompanyCosts)
                    .HasColumnName("carrier_company_costs")
                    .HasColumnType("decimal(17, 2)");

                entity.Property(e => e.DeportingTime).HasColumnName("deporting_time");

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Journey)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__BusDriver__bus_i__4222D4EF");

                entity.HasOne(d => d.Driver)
                    .WithMany(p => p.Journey)
                    .HasForeignKey(d => d.DriverId)
                    .HasConstraintName("FK__BusDriver__drive__412EB0B6");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Journey)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK__BusDriver__route__4316F928");
            });

            modelBuilder.Entity<JourneyRoutePoint>(entity =>
            {
                entity.HasKey(e => e.TrpId)
                    .HasName("PK__JourneyR__51881C5ADB0471A4");

                entity.Property(e => e.TrpId).HasColumnName("trp_id");

                entity.Property(e => e.ArrivalTime).HasColumnName("arrival_time");

                entity.Property(e => e.JourneyId).HasColumnName("journey_id");

                entity.Property(e => e.RpId).HasColumnName("rp_id");

                entity.Property(e => e.TicketPrice)
                    .HasColumnName("ticket_price")
                    .HasColumnType("decimal(6, 2)");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.JourneyRoutePoint)
                    .HasForeignKey(d => d.JourneyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JourneyRo__journ__0697FACD");

                entity.HasOne(d => d.Rp)
                    .WithMany(p => p.JourneyRoutePoint)
                    .HasForeignKey(d => d.RpId)
                    .HasConstraintName("FK__JourneyRo__rp_id__7849DB76");
            });

            modelBuilder.Entity<Mechanic>(entity =>
            {
                entity.Property(e => e.MechanicId).HasColumnName("mechanic_id");

                entity.Property(e => e.EmployerId).HasColumnName("employer_id");

                entity.Property(e => e.Qualification)
                    .IsRequired()
                    .HasColumnName("qualification")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Mechanic)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK__Mechanic__employ__797309D9");
            });

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.Property(e => e.PassengerAge).HasColumnName("passenger_age");

                entity.Property(e => e.PassengerName)
                    .IsRequired()
                    .HasColumnName("passenger_name")
                    .HasMaxLength(4)
                    .IsUnicode(false);

                entity.Property(e => e.Preferential).HasColumnName("preferential");
            });

            modelBuilder.Entity<RoutePoint>(entity =>
            {
                entity.HasKey(e => e.RpId)
                    .HasName("PK__RoutePoi__CBB62532376C9659");

                entity.Property(e => e.RpId).HasColumnName("rp_id");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("city_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.StopName)
                    .IsRequired()
                    .HasColumnName("stop_name")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<RouteRoutePoint>(entity =>
            {
                entity.HasKey(e => e.RrpId)
                    .HasName("PK__TicketRo__51881C5AF571C05A");

                entity.Property(e => e.RrpId).HasColumnName("rrp_id");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.RpId).HasColumnName("rp_id");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RouteRoutePoint)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK__RouteRout__route__73852659");

                entity.HasOne(d => d.Rp)
                    .WithMany(p => p.RouteRoutePoint)
                    .HasForeignKey(d => d.RpId)
                    .HasConstraintName("FK__RouteRout__rp_id__74794A92");
            });

            modelBuilder.Entity<StationPlatform>(entity =>
            {
                entity.HasKey(e => e.SpId)
                    .HasName("PK__StationP__4C367CEEDB307028");

                entity.Property(e => e.SpId).HasColumnName("sp_id");

                entity.Property(e => e.SpNumber).HasColumnName("sp_number");

                entity.Property(e => e.WorldPart)
                    .HasColumnName("world_part")
                    .HasMaxLength(6)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<StorageCenter>(entity =>
            {
                entity.HasKey(e => e.SCenterId)
                    .HasName("PK__StorageC__8E1A74A270E9D582");

                entity.Property(e => e.SCenterId).HasColumnName("s_center_id");

                entity.Property(e => e.AccountingId).HasColumnName("accounting_id");

                entity.Property(e => e.EndPeriod)
                    .HasColumnName("end_period")
                    .HasColumnType("date");

                entity.Property(e => e.PlaceNumber).HasColumnName("place_number");

                entity.Property(e => e.StartPerion)
                    .HasColumnName("start_perion")
                    .HasColumnType("date");

                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");

                entity.HasOne(d => d.Accounting)
                    .WithMany(p => p.StorageCenter)
                    .HasForeignKey(d => d.AccountingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StorageCe__accou__6EF57B66");
            });

            modelBuilder.Entity<StoreBaggage>(entity =>
            {
                entity.HasKey(e => e.SbId)
                    .HasName("PK__StoreBag__EABDD60E650B31CE");

                entity.Property(e => e.SbId).HasColumnName("sb_id");

                entity.Property(e => e.BaggageId).HasColumnName("baggage_id");

                entity.Property(e => e.SCenterId).HasColumnName("s_center_id");

                entity.Property(e => e.SbHours).HasColumnName("sb_hours");

                entity.Property(e => e.SbNumber).HasColumnName("sb_number");

                entity.Property(e => e.SbVolume).HasColumnName("sb_volume");

                entity.Property(e => e.SbWeight).HasColumnName("sb_weight");

                entity.Property(e => e.TotalAmount).HasColumnName("total_amount");

                entity.HasOne(d => d.Baggage)
                    .WithMany(p => p.StoreBaggage)
                    .HasForeignKey(d => d.BaggageId)
                    .HasConstraintName("FK__StoreBagg__bagga__76969D2E");

                entity.HasOne(d => d.SCenter)
                    .WithMany(p => p.StoreBaggage)
                    .HasForeignKey(d => d.SCenterId)
                    .HasConstraintName("FK__StoreBagg__s_cen__75A278F5");
            });

            modelBuilder.Entity<StoreKeeper>(entity =>
            {
                entity.HasKey(e => e.SKeeperId)
                    .HasName("PK__StoreKee__3645B5F452DA66CF");

                entity.Property(e => e.SKeeperId).HasColumnName("s_keeper_id");

                entity.Property(e => e.EmployerId).HasColumnName("employer_id");

                entity.Property(e => e.SCenterId).HasColumnName("s_center_id");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.StoreKeeper)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK__StoreKeep__emplo__71D1E811");

                entity.HasOne(d => d.SCenter)
                    .WithMany(p => p.StoreKeeper)
                    .HasForeignKey(d => d.SCenterId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__StoreKeep__s_cen__72C60C4A");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.CtId).HasColumnName("ct_id");

                entity.Property(e => e.Nds).HasColumnName("nds");

                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.Property(e => e.RpId).HasColumnName("rp_id");

                entity.Property(e => e.Seat).HasColumnName("seat");

                entity.Property(e => e.TripId).HasColumnName("trip_id");

                entity.HasOne(d => d.Ct)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.CtId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__ct_id__6B24EA82");

                entity.HasOne(d => d.Passenger)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.PassengerId)
                    .HasConstraintName("FK__Ticket__passenge__693CA210");

                entity.HasOne(d => d.Rp)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.RpId)
                    .HasConstraintName("FK__Ticket__rp_id__078C1F06");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TripId)
                    .HasConstraintName("FK__Ticket__trip_id__607251E5");
            });

            modelBuilder.Entity<TransportationCosts>(entity =>
            {
                entity.HasKey(e => e.CsId)
                    .HasName("PK__Transpor__138C55F4CB7B27FB");

                entity.Property(e => e.CsId).HasColumnName("cs_id");

                entity.Property(e => e.AccountingId).HasColumnName("accounting_id");

                entity.Property(e => e.CarrierComCost).HasColumnName("carrier_com_cost");

                entity.Property(e => e.FuelCosts).HasColumnName("fuel_costs");

                entity.Property(e => e.JourneyId).HasColumnName("journey_id");

                entity.Property(e => e.MechanicCosts).HasColumnName("mechanic_costs");

                entity.Property(e => e.Taxes).HasColumnName("taxes");

                entity.HasOne(d => d.Accounting)
                    .WithMany(p => p.TransportationCosts)
                    .HasForeignKey(d => d.AccountingId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Transport__accou__4E88ABD4");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.TransportationCosts)
                    .HasForeignKey(d => d.JourneyId)
                    .HasConstraintName("FK__Transport__bdr_i__4F7CD00D");
            });

            modelBuilder.Entity<Trip>(entity =>
            {
                entity.Property(e => e.TripId).HasColumnName("trip_id");

                entity.Property(e => e.DeportingDate)
                    .HasColumnName("deporting_date")
                    .HasColumnType("date");

                entity.Property(e => e.DeportingStat)
                    .IsRequired()
                    .HasColumnName("deporting_stat")
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('PLANNED')");

                entity.Property(e => e.DispatcherId).HasColumnName("dispatcher_id");

                entity.Property(e => e.JourneyId).HasColumnName("journey_id");

                entity.Property(e => e.PassangersCount).HasColumnName("passangers_count");

                entity.HasOne(d => d.Dispatcher)
                    .WithMany(p => p.Trip)
                    .HasForeignKey(d => d.DispatcherId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Journey__dispatc__00200768");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.Trip)
                    .HasForeignKey(d => d.JourneyId)
                    .HasConstraintName("FK__Journey__bdr_id__282DF8C2");
            });

            modelBuilder.Entity<Vacation>(entity =>
            {
                entity.Property(e => e.VacationId).HasColumnName("vacation_id");

                entity.Property(e => e.EmployerId).HasColumnName("employer_id");

                entity.Property(e => e.LeaveDate)
                    .HasColumnName("leave_date")
                    .HasColumnType("date");

                entity.Property(e => e.PayLeave)
                    .HasColumnName("pay_leave")
                    .HasColumnType("decimal(17, 2)");

                entity.Property(e => e.ReturnDate)
                    .HasColumnName("return_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Vacation)
                    .HasForeignKey(d => d.EmployerId)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK__Vacation__employ__10566F31");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
