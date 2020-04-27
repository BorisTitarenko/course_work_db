using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace cource_work.Models.Entity
{
    public partial class buz_stationContext : DbContext
    {
        public buz_stationContext()
        {
        }

        public buz_stationContext(DbContextOptions<buz_stationContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Accounting> Accounting { get; set; }
        public virtual DbSet<Broute> Broute { get; set; }
        public virtual DbSet<Bus> Bus { get; set; }
        public virtual DbSet<CarrierCompany> CarrierCompany { get; set; }
        public virtual DbSet<CashTransaction> CashTransaction { get; set; }
        public virtual DbSet<DayCashAmount> DayCashAmount { get; set; }
        public virtual DbSet<Driver> Driver { get; set; }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Euser> Euser { get; set; }
        public virtual DbSet<Journey> Journey { get; set; }
        public virtual DbSet<JourneyRoutePoint> JourneyRoutePoint { get; set; }
        public virtual DbSet<Passenger> Passenger { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<RoutePoint> RoutePoint { get; set; }
        public virtual DbSet<RouteRoutePoint> RouteRoutePoint { get; set; }
        public virtual DbSet<Salary> Salary { get; set; }
        public virtual DbSet<Ticket> Ticket { get; set; }
        public virtual DbSet<TransportationCosts> TransportationCosts { get; set; }
        public virtual DbSet<Trip> Trip { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-AHJJ5GR\\MYDB;Initial catalog=buz_station;Integrated security=False;User Id=huy;Password=huyhuy;MultipleActiveResultSets=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounting>(entity =>
            {
                entity.HasKey(e => e.AccId)
                    .HasName("PK__Accounti__9A20D554708C0479");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.EndPerion)
                    .HasColumnName("end_perion")
                    .HasColumnType("date");

                entity.Property(e => e.Pdv)
                    .HasColumnName("pdv")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Pp)
                    .HasColumnName("pp")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.SalaryAmount)
                    .HasColumnName("salary_amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.StartPerion)
                    .HasColumnName("start_perion")
                    .HasColumnType("date");

                entity.Property(e => e.TicketAmount)
                    .HasColumnName("ticket_amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.TransportationAmount)
                    .HasColumnName("transportation_amount")
                    .HasColumnType("decimal(18, 2)");
            });

            modelBuilder.Entity<Broute>(entity =>
            {
                entity.HasKey(e => e.RouteId)
                    .HasName("PK__BRoute__28F706FE2C3EFAFD");

                entity.ToTable("BRoute");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.EndPoint)
                    .IsRequired()
                    .HasColumnName("end_point")
                    .HasMaxLength(30);

                entity.Property(e => e.RouteLength).HasColumnName("route_length");

                entity.Property(e => e.StartPoint)
                    .IsRequired()
                    .HasColumnName("start_point")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<Bus>(entity =>
            {
                entity.Property(e => e.BusId).HasColumnName("bus_id");

                entity.Property(e => e.BusModel)
                    .IsRequired()
                    .HasColumnName("bus_model")
                    .HasMaxLength(30);

                entity.Property(e => e.CcId).HasColumnName("cc_id");

                entity.Property(e => e.ClimateControl).HasColumnName("climate_control");

                entity.Property(e => e.SitingNumber).HasColumnName("siting_number");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.Bus)
                    .HasForeignKey(d => d.CcId)
                    .HasConstraintName("FK__Bus__cc_id__09A971A2");
            });

            modelBuilder.Entity<CarrierCompany>(entity =>
            {
                entity.HasKey(e => e.CcId)
                    .HasName("PK__CarrierC__9F1E187BC1EDE395");

                entity.HasIndex(e => e.CcName)
                    .HasName("UQ__CarrierC__94C7136296D0CC63")
                    .IsUnique();

                entity.Property(e => e.CcId).HasColumnName("cc_id");

                entity.Property(e => e.CcName)
                    .IsRequired()
                    .HasColumnName("cc_name")
                    .HasMaxLength(40);

                entity.Property(e => e.CcOwner)
                    .HasColumnName("cc_owner")
                    .HasMaxLength(40);

                entity.Property(e => e.CcPhone)
                    .IsRequired()
                    .HasColumnName("cc_phone")
                    .HasMaxLength(14);

                entity.Property(e => e.OfficeAdress)
                    .HasColumnName("office_adress")
                    .HasMaxLength(40);
            });

            modelBuilder.Entity<CashTransaction>(entity =>
            {
                entity.HasKey(e => e.CtId)
                    .HasName("PK__CashTran__33D47D09BE0978BB");

                entity.Property(e => e.CtId).HasColumnName("ct_id");

                entity.Property(e => e.DcaId).HasColumnName("dca_id");

                entity.Property(e => e.PayTime).HasColumnName("pay_time");

                entity.Property(e => e.TotalCash)
                    .HasColumnName("total_cash")
                    .HasColumnType("decimal(14, 2)");

                entity.Property(e => e.WithCart).HasColumnName("with_cart");

                entity.HasOne(d => d.Dca)
                    .WithMany(p => p.CashTransaction)
                    .HasForeignKey(d => d.DcaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__CashTrans__dca_i__66603565");
            });

            modelBuilder.Entity<DayCashAmount>(entity =>
            {
                entity.HasKey(e => e.DcaId)
                    .HasName("PK__DayCashA__3726447B91C1DD3B");

                entity.Property(e => e.DcaId).HasColumnName("dca_id");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.LastTransactionTime).HasColumnName("last_transaction_time");

                entity.Property(e => e.TotalDayAmount)
                    .HasColumnName("total_day_amount")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.WorkDate)
                    .HasColumnName("work_date")
                    .HasColumnType("date");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.DayCashAmount)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DayCashAm__acc_i__628FA481");
            });

            modelBuilder.Entity<Driver>(entity =>
            {
                entity.HasIndex(e => e.DriverLicence)
                    .HasName("UQ__Driver__8BAB4049518AFBD4")
                    .IsUnique();

                entity.HasIndex(e => e.DriverPassport)
                    .HasName("UQ__Driver__57B10A90046E5AFB")
                    .IsUnique();

                entity.Property(e => e.DriverId).HasColumnName("driver_id");

                entity.Property(e => e.CcId).HasColumnName("cc_id");

                entity.Property(e => e.DriverAdress)
                    .IsRequired()
                    .HasColumnName("driver_adress")
                    .HasMaxLength(40);

                entity.Property(e => e.DriverBirthDate)
                    .HasColumnName("driver_birth_date")
                    .HasColumnType("date");

                entity.Property(e => e.DriverEmpHistory)
                    .IsRequired()
                    .HasColumnName("driver_emp_history")
                    .HasMaxLength(20);

                entity.Property(e => e.DriverIdenCode)
                    .IsRequired()
                    .HasColumnName("driver_iden_code")
                    .HasMaxLength(20);

                entity.Property(e => e.DriverLicence)
                    .IsRequired()
                    .HasColumnName("driver_licence")
                    .HasMaxLength(40);

                entity.Property(e => e.DriverName)
                    .HasColumnName("driver_name")
                    .HasMaxLength(30);

                entity.Property(e => e.DriverPassport)
                    .IsRequired()
                    .HasColumnName("driver_passport")
                    .HasMaxLength(90);

                entity.Property(e => e.DriverPhone)
                    .IsRequired()
                    .HasColumnName("driver_phone")
                    .HasMaxLength(14);

                entity.Property(e => e.DriverSalary).HasColumnName("driver_salary");

                entity.Property(e => e.LikeShanson).HasColumnName("like_shanson");

                entity.HasOne(d => d.Cc)
                    .WithMany(p => p.Driver)
                    .HasForeignKey(d => d.CcId)
                    .HasConstraintName("FK__Driver__cc_id__0A9D95DB");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EmployeeName)
                    .IsRequired()
                    .HasColumnName("employee_name")
                    .HasMaxLength(30);

                entity.Property(e => e.EmployeePassport)
                    .IsRequired()
                    .HasColumnName("employee_passport")
                    .HasMaxLength(20);

                entity.Property(e => e.EmployeePhone)
                    .IsRequired()
                    .HasColumnName("employee_phone")
                    .HasMaxLength(13);

                entity.Property(e => e.EmployeeWorkBook)
                    .HasColumnName("employee_work_book")
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<Euser>(entity =>
            {
                entity.ToTable("EUser");

                entity.Property(e => e.EuserId).HasColumnName("euser_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EuserLogin)
                    .IsRequired()
                    .HasColumnName("euser_login")
                    .HasMaxLength(20);

                entity.Property(e => e.EuserPassword)
                    .IsRequired()
                    .HasColumnName("euser_password")
                    .HasMaxLength(20);

                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Euser)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__EUser__employee___6477ECF3");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Euser)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__EUser__role_id__0C85DE4D");
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

                entity.Property(e => e.EndTime).HasColumnName("end_time");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.StartTime).HasColumnName("start_time");

                entity.HasOne(d => d.Bus)
                    .WithMany(p => p.Journey)
                    .HasForeignKey(d => d.BusId)
                    .HasConstraintName("FK__Journey__bus_id__66603565");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.Journey)
                    .HasForeignKey(d => d.RouteId)
                    .HasConstraintName("FK__Journey__route_i__6754599E");
            });

            modelBuilder.Entity<JourneyRoutePoint>(entity =>
            {
                entity.HasKey(e => e.TrpId)
                    .HasName("PK__JourneyR__51881C5A529EFE9D");

                entity.Property(e => e.TrpId).HasColumnName("trp_id");

                entity.Property(e => e.ArrivalTime).HasColumnName("arrival_time");

                entity.Property(e => e.JourneyId).HasColumnName("journey_id");

                entity.Property(e => e.Pdv)
                    .HasColumnName("pdv")
                    .HasColumnType("decimal(6, 2)");

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

            modelBuilder.Entity<Passenger>(entity =>
            {
                entity.Property(e => e.PassengerId).HasColumnName("passenger_id");

                entity.Property(e => e.PassengerAge).HasColumnName("passenger_age");

                entity.Property(e => e.PassengerName)
                    .IsRequired()
                    .HasColumnName("passenger_name")
                    .HasMaxLength(40);

                entity.Property(e => e.Preferential).HasColumnName("preferential");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.RoleId).HasColumnName("role_id");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasColumnName("role_name")
                    .HasMaxLength(10);
            });

            modelBuilder.Entity<RoutePoint>(entity =>
            {
                entity.HasKey(e => e.RpId)
                    .HasName("PK__RoutePoi__CBB62532F3F4A37E");

                entity.Property(e => e.RpId).HasColumnName("rp_id");

                entity.Property(e => e.CityName)
                    .IsRequired()
                    .HasColumnName("city_name")
                    .HasMaxLength(30);

                entity.Property(e => e.StopName)
                    .IsRequired()
                    .HasColumnName("stop_name")
                    .HasMaxLength(30);
            });

            modelBuilder.Entity<RouteRoutePoint>(entity =>
            {
                entity.HasKey(e => e.RrpId)
                    .HasName("PK__RouteRou__3F7A7E5753085B89");

                entity.Property(e => e.RrpId).HasColumnName("rrp_id");

                entity.Property(e => e.RouteId).HasColumnName("route_id");

                entity.Property(e => e.RpId).HasColumnName("rp_id");

                entity.HasOne(d => d.Route)
                    .WithMany(p => p.RouteRoutePoint)
                    .HasForeignKey(d => d.RouteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RouteRout__route__0F624AF8");

                entity.HasOne(d => d.Rp)
                    .WithMany(p => p.RouteRoutePoint)
                    .HasForeignKey(d => d.RpId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__RouteRout__rp_id__10566F31");
            });

            modelBuilder.Entity<Salary>(entity =>
            {
                entity.Property(e => e.SalaryId).HasColumnName("salary_id");

                entity.Property(e => e.AccId).HasColumnName("acc_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.EmployeeSalary)
                    .HasColumnName("employee_salary")
                    .HasColumnType("decimal(18, 2)");

                entity.HasOne(d => d.Acc)
                    .WithMany(p => p.Salary)
                    .HasForeignKey(d => d.AccId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Salary__acc_id__336AA144");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Salary)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Salary__employee__114A936A");
            });

            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.TicketId).HasColumnName("ticket_id");

                entity.Property(e => e.CtId).HasColumnName("ct_id");

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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__rp_id__078C1F06");

                entity.HasOne(d => d.Trip)
                    .WithMany(p => p.Ticket)
                    .HasForeignKey(d => d.TripId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Ticket__trip_id__607251E5");
            });

            modelBuilder.Entity<TransportationCosts>(entity =>
            {
                entity.HasKey(e => e.CsId)
                    .HasName("PK__Transpor__138C55F4DFAAF755");

                entity.Property(e => e.CsId).HasColumnName("cs_id");

                entity.Property(e => e.AccountingId).HasColumnName("accounting_id");

                entity.Property(e => e.CarrierComCost)
                    .HasColumnName("carrier_com_cost")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.FuelCosts)
                    .HasColumnName("fuel_costs")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Inssurance)
                    .HasColumnName("inssurance")
                    .HasColumnType("decimal(18, 2)");

                entity.Property(e => e.JourneyId).HasColumnName("journey_id");

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
                    .HasDefaultValueSql("('PLANNED')");

                entity.Property(e => e.JourneyId).HasColumnName("journey_id");

                entity.Property(e => e.PassangersCount).HasColumnName("passangers_count");

                entity.HasOne(d => d.Journey)
                    .WithMany(p => p.Trip)
                    .HasForeignKey(d => d.JourneyId)
                    .HasConstraintName("FK__Trip__journey_id__73BA3083");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
