using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem
{
	public class Address(int id, string streetNumber, string streetName, string suburb, string state, string postcode)
	{
		[Key]
		public int Id { get; set; } = id;

		public string StreetNumber { get; set; } = streetNumber;

		public string StreetName { get; set; } = streetName;

		public string Suburb { get; set; } = suburb;

		public string State { get; set; } = state;

		public string Postcode { get; set; } = postcode;

		public override string ToString()
		{
			return $"{StreetName} {StreetName} {Suburb} {State} {Postcode}";
		}
	}

}