using System.Linq.Expressions;

namespace HospitalManagementSystem
{
	public class AddressRepository : IRepository<Address>
	{
		protected readonly HospitalManagementSystemContext dbContext;

		public AddressRepository(HospitalManagementSystemContext context)
		{
			dbContext = context;
		}

		/// <summary>
		/// Adds an Address to the database
		/// </summary>
		/// <param name="entity"></param>
		public void Add(Address entity)
		{
			dbContext.Set<Address>().Add(entity);
		}

		/// <summary>
		/// Adds multiple instances of Addresses to the database
		/// </summary>
		/// <param name="entities"></param>
		public void AddRange(IEnumerable<Address> entities)
		{
			dbContext.Set<Address>().AddRange(entities);
		}

		/// <summary>
		/// Returns all instances of Addresses that satisfy the predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public IEnumerable<Address> Find(Expression<Func<Address, bool>> predicate)
		{
			return dbContext.Set<Address>().Where(predicate);
		}

		/// <summary>
		/// Returns all instances of Addresses in the database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<Address> GetAll()
		{
			return dbContext.Set<Address>();
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