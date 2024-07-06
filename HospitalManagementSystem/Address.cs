namespace HospitalManagementSystem
{
	public class Address(string streetNumber, string streetName, string suburb, string state, string postcode)
	{
		public string StreetNumber = streetNumber;
		public string StreetName = streetName;
		public string Suburb = suburb;
		public string State = state;
		public string Postcode = postcode;

		public override string ToString()
		{
			return $"{StreetName} {StreetName} {Suburb} {State} {Postcode}";
		}
	}

}