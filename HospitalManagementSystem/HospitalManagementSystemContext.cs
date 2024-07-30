using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem
{
	public class HospitalManagementSystemContext : DbContext
	{
		public virtual DbSet<User> Users { get; set; }

		public virtual DbSet<Address> Addresses { get; set; }

		public virtual DbSet<Appointment> Appointments { get; set; }

		public string DbPath { get; }

		public HospitalManagementSystemContext()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "library.db");
		}

		/// <summary>
		/// Sets connection string for DB
		/// </summary>
		/// <param name="optionsBuilder"></param>
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source={DbPath}");
		}

		/// <summary>
		/// Defines the foreign key relationships between all tables in the DB
		/// </summary>
		/// <param name="modelBuilder"></param>
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<User>()
				.HasDiscriminator<string>("UserType")
				.HasValue<Admin>("Admin")
				.HasValue<HospitalUser>("HospitalUser");

			modelBuilder.Entity<HospitalUser>()
				.HasDiscriminator<string>("HospitalUserType")
				.HasValue<Doctor>("Doctor")
				.HasValue<Patient>("Patient");

			modelBuilder.Entity<HospitalUser>()
				.HasOne(u => u.Address)
				.WithMany(a => a.HospitalUsers)
				.HasForeignKey(u => u.AddressId);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Doctor)
				.WithMany(d => d.Appointments)
				.HasForeignKey(a => a.DoctorId);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Patient)
				.WithMany(p => p.Appointments)
				.HasForeignKey(a => a.PatientId);

			modelBuilder.Entity<Patient>()
				.HasOne(u => u.Doctor)
				.WithMany(a => a.Patients)
				.HasForeignKey(u => u.DoctorId);
		}
	}
}
