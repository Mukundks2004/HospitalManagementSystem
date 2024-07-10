namespace HospitalManagementSystem
{
	public abstract class User(int id, string password, string firstname, string lastname, Address? address, string? email, string? phone)
	{
		public int Id { get; set; } = id;

		public string Password { get; set; } = password;

		public string Firstname { get; set; } = firstname;

		public string Lastname { get; set; } = lastname;

		public Address? Address { get; set; } = address;

		public string? Email { get; set; } = email;

		public string? Phone { get; set; } = phone;

		public override abstract string ToString();

		public abstract void DisplayMenu();

		public string GetFullName()
		{
			return $"{Firstname} {Lastname}";
		}
	}

}