namespace HospitalManagementSystem
{
	public static class AddressExtensions
	{
		public static string GetString(this Address address)
		{
			return $"{address.StreetNumber} {address.StreetName} {address.Suburb} {address.State} {address.Postcode}";
		}
	}

}