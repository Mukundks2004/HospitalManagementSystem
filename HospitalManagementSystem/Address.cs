using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem
{
	public class Address
	{
		[Key]
		public int? Id { get; set; }

		public string? StreetNumber { get; set; }

		public string? StreetName { get; set; }

		public string? Suburb { get; set; }

		public string? State { get; set; }

		public string? Postcode { get; set; }

		public ICollection<HospitalUser>? HospitalUsers { get; set; }

		/// <summary>
		/// Returns string representation of an Address
		/// </summary>
		/// <param name="address"></param>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{StreetNumber} {StreetName} {Suburb} {State} {Postcode}";
		}
	}
}