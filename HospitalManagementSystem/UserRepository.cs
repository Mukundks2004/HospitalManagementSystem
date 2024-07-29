using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem
{
	public class UserRepository : Repository<User>
	{
		public UserRepository(HospitalManagementSystemContext context) : base(context)
		{
		}

		public Doctor? GetDoctorById(int id)
		{
			return dbContext.Users.OfType<Doctor>().Where(u => u.Id == id).Include(u => u.Address).FirstOrDefault();
		}

		public Patient? GetPatientById(int id)
		{
			return dbContext.Users.OfType<Patient>().Where(u => u.Id == id).Include(u => u.Address).FirstOrDefault();
		}

		public IEnumerable<Doctor> GetAllDoctors()
		{
			return dbContext.Users.OfType<Doctor>().Include(u => u.Address);
		}

		public IEnumerable<Patient> GetAllPatients()
		{
			return dbContext.Users.OfType<Patient>().Include(u => u.Address);
		}

		public IEnumerable<Admin> GetAllAdmin()
		{
			return dbContext.Users.OfType<Admin>();
		}

		public IEnumerable<HospitalUser> GetAllHospitalUsers()
		{
			return dbContext.Users.OfType<HospitalUser>().Include(u => u.Address);
		}
	}
}