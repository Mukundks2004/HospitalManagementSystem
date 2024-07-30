using System.Linq.Expressions;

namespace HospitalManagementSystem
{
	public interface IRepository<T> where T : class
	{
		T? GetById(int id);

		IEnumerable<T> GetAll();

		void Add(T entity);

		void AddRange(IEnumerable<T> entities);

		IEnumerable<T> Find(Expression<Func<T, bool>> predicate);

		int SaveChanges();
	}
}