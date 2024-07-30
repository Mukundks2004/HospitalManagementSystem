namespace HospitalManagementSystem
{
	public class DbInitializer
	{
		readonly HospitalManagementSystemContext dbContext;

		public DbInitializer(HospitalManagementSystemContext context)
		{
			dbContext = context;
		}

		public void DatabaseRefresh()
		{
			dbContext.Database.EnsureDeleted();
			dbContext.Database.EnsureCreated();
		}
		
	}
}