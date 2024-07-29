namespace HospitalManagementSystem
{
	public abstract class HospitalUser : User
	{
		public int? AddressId { get; set; }

		public Address? Address { get; set; }

		public string? Email { get; set; }

		public string? Phone { get; set; }
	}
}