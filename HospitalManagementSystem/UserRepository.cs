using Microsoft.EntityFrameworkCore;

namespace HospitalManagementSystem
{
	public class UserRepository : Repository<User>
	{
		public UserRepository(HospitalManagementSystemContext context) : base(context)
		{
		}

		/// <summary>
		/// Returns the doctor whose ID matches the provided id, or null otherwise. includes addresses.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Doctor? GetDoctorById(int id)
		{
			return dbContext.Users.OfType<Doctor>().Where(u => u.Id == id).Include(u => u.Address).FirstOrDefault();
		}

		/// <summary>
		/// Returns the patient whose ID matches the provided id, or null otherwise. includes addresses.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public Patient? GetPatientById(int id)
		{
			return dbContext.Users.OfType<Patient>().Where(u => u.Id == id).Include(u => u.Address).FirstOrDefault();
		}

		/// <summary>
		/// Returns all doctors and their addresses
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Doctor> GetAllDoctors()
		{
			return dbContext.Users.OfType<Doctor>().Include(u => u.Address);
		}

		/// <summary>
		/// Returns all patients and their addresses
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Patient> GetAllPatients()
		{
			return dbContext.Users.OfType<Patient>().Include(u => u.Address);
		}

		/// <summary>
		/// Returns all admin
		/// </summary>
		/// <returns></returns>
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