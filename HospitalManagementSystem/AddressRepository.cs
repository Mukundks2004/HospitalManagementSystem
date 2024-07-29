namespace HospitalManagementSystem
{
	public class AddressRepository : Repository<Address>
	{
		public AddressRepository(HospitalManagementSystemContext context) : base(context)
		{
		}
	}
}