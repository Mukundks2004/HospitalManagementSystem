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

		public void Add(T entity)
		{
			dbContext.Set<T>().Add(entity);
		}

		public void AddRange(IEnumerable<T> entities)
		{
			dbContext.Set<T>().AddRange(entities);
		}

		public IEnumerable<T> Find(Expression<Func<T, bool>> predicate)
		{
			return dbContext.Set<T>().Where(predicate);
		}

		public IEnumerable<T> GetAll()
		{
			return dbContext.Set<T>();
		}

		public T? GetById(int id)
		{
			return dbContext.Set<T>().Find(id);
		}

		public void Update(T entity)
		{
			dbContext.Set<T>().Update(entity);
		}

		public void Remove(T entity)
		{
			dbContext.Set<T>().Remove(entity);
		}

		public void SaveChanges()
		{
			dbContext.SaveChanges();
		}
	}
}