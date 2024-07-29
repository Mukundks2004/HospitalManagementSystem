using System.Linq.Expressions;

namespace HospitalManagementSystem
{
	public class Repository<T> : IRepository<T> where T : class
	{
		protected readonly HospitalManagementSystemContext dbContext;

		public Repository(HospitalManagementSystemContext dbContext)
		{
			this.dbContext = dbContext;
		}

		/// <summary>
		/// Adds an entity T to the database
		/// </summary>
		/// <param name="entity"></param>
		public void Add(T entity)
		{
			dbContext.Set<T>().Add(entity);
		}

		/// <summary>
		/// Adds multiple instances of T to the database
		/// </summary>
		/// <param name="entities"></param>
		public void AddRange(IEnumerable<T> entities)
		{
			dbContext.Set<T>().AddRange(entities);
		}

		/// <summary>
		/// Returns all instances of T that satisfy the predicate
		/// </summary>
		/// <param name="predicate"></param>
		/// <returns></returns>
		public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
		{
			return dbContext.Set<T>().Where(predicate);
		}

		/// <summary>
		/// Returns all instances of T in the database
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> GetAll()
		{
			return dbContext.Set<T>();
		}

		/// <summary>
		/// Returns the first T that matches the provided ID, or null if none do
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public T? GetById(int id)
		{
			return dbContext.Set<T>().Find(id);
		}

		/// <summary>
		/// Begins tracking the entity provided and all reachable entities
		/// </summary>
		/// <param name="entity"></param>
		public void Update(T entity)
		{
			dbContext.Set<T>().Update(entity);
		}

		/// <summary>
		/// Removes T from the database
		/// </summary>
		/// <param name="entity"></param>
		public void Remove(T entity)
		{
			dbContext.Set<T>().Remove(entity);
		}

		/// <summary>
		/// Saves any changes that have been made to the database including adding, removing and updating entries.
		/// </summary>
		public void SaveChanges()
		{
			dbContext.SaveChanges();
		}
	}
}