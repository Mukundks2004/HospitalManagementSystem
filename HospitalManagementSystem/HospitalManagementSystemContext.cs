using System.Data.Entity;

namespace HospitalManagementSystem
{
	public class HospitalManagementSystemContext : DbContext
	{
		public DbSet<Admin> Admins { get; set; }

		public DbSet<Patient> Patients { get; set; }

		public DbSet<Doctor> Doctors { get; set; }

		public DbSet<Address> Addresses { get; set; }

		public DbSet<Appointment> Appointments { get; set; }
	}
}
