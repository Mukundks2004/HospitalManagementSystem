using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem
{
	public class HospitalManagementSystemContext : DbContext
	{
		public DbSet<User> Users { get; set; }

		public DbSet<Address> Addresses { get; set; }

		public DbSet<Appointment> Appointments { get; set; }

		public string DbPath { get; }

		public HospitalManagementSystemContext()
		{
			var folder = Environment.SpecialFolder.LocalApplicationData;
			var path = Environment.GetFolderPath(folder);
			DbPath = System.IO.Path.Join(path, "library.db");

			Database.EnsureDeleted();
			Database.EnsureCreated();
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseSqlite($"Data Source={DbPath}");
		}

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
				//.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Doctor)
				.WithMany(d => d.Appointments)
				.HasForeignKey(a => a.DoctorId);
				//.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Appointment>()
				.HasOne(a => a.Patient)
				.WithMany(p => p.Appointments)
				.HasForeignKey(a => a.PatientId);
			//.OnDelete(DeleteBehavior.Cascade);

			modelBuilder.Entity<Patient>()
				.HasOne(u => u.Doctor)
				.WithMany(a => a.Patients)
				.HasForeignKey(u => u.DoctorId);
				//.OnDelete(DeleteBehavior.Cascade);
		}
	}
}
