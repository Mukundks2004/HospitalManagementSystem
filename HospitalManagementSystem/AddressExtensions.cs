namespace HospitalManagementSystem
{
	public static class AddressExtensions
	{
		/// <summary>
		/// Returns string representation of an Address
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public static string GetString(this Address address)
		{
			return $"{address.StreetNumber} {address.StreetName} {address.Suburb} {address.State} {address.Postcode}";
		}
	}

}