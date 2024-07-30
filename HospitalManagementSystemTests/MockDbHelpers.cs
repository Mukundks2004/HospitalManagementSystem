using Microsoft.EntityFrameworkCore;
using Moq;

namespace HospitalManagementSystemTests
{
	internal static class MockDbHelpers
	{
		public static DbSet<T> GetQueryableMockDbSet<T>(List<T> sourceList) where T : class
		{
			var queryable = sourceList.AsQueryable();

			var dbSet = new Mock<DbSet<T>>();
			dbSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
			dbSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
			dbSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
			dbSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

			dbSet.Setup(d => d.Add(It.IsAny<T>())).Callback<T>((s) => sourceList.Add(s));
			dbSet.Setup(d => d.AddRange(It.IsAny<IEnumerable<T>>())).Callback<IEnumerable<T>>((s) => sourceList.AddRange(s));
			return dbSet.Object;
		}
	}
}