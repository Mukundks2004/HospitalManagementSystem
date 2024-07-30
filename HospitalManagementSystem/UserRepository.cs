using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace HospitalManagementSystem
{
	public class UserRepository : IRepository<User>
	{
		protected readonly HospitalManagementSystemContext dbContext;

		public UserRepository(HospitalManagementSystemContext context)
		{
			dbContext = context;
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

		/// <summary>
		/// returns all doctors and patients
		/// </summary>
		/// <returns></returns>
		public IEnumerable<HospitalUser> GetAllHospitalUsersWithAddress()
		{
			return dbContext.Users.OfType<HospitalUser>().Include(u => u.Address);
		}

		/// <summary>
		/// Adds a User to the database
		/// </summary>
		/// <param name="entity"></param>
		public void Add(User entity)
		{
			dbContext.Set<User>().Add(entity);
		}

		/// <summary>
		/// Adds multiple instances of Users to the database
		/// </summary>
		/// <param name="entities"></param>
		public void AddRange(IEnumerable<User> entities)
		{
			dbContext.Set<User>().AddRange(entities);
		}

		/// <summary>
		/// Returns all instances of Users that satisfy the predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public IEnumerable<User> Find(Expression<Func<User, bool>> predicate)
		{
			return dbContext.Set<User>().Where(predicate);
		}

		/// <summary>
		/// Returns all instances of Users in the database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<User> GetAll()
		{
			return dbContext.Set<User>();
		}

		/// <summary>
		/// Saves any changes that have been made to the database including adding, removing and updating entries.
		/// </summary>
		public int SaveChanges()
		{
			return dbContext.SaveChanges();
		}
	}
}