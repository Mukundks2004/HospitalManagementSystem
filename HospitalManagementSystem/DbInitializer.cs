namespace HospitalManagementSystem
{
	public class DbInitializer
	{
		readonly HospitalManagementSystemContext dbContext;

		public DbInitializer(HospitalManagementSystemContext context)
		{
			dbContext = context;
		}

		/// <summary>
		/// Deletes database and recreates it with same schema
		/// </summary>
		public void DatabaseRefresh()
		{
			dbContext.Database.EnsureDeleted();
			dbContext.Database.EnsureCreated();
		}
		
	}
}