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
		/// Deletes database
		/// </summary>
		public void DatabaseDelete()
		{
			dbContext.Database.EnsureDeleted();
		}

		/// <summary>
		/// Creates database
		/// </summary>
		public void DatabaseCreate()
		{
			dbContext.Database.EnsureCreated();
		}

	}
}