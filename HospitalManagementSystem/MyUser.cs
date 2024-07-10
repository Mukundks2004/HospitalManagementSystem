namespace HospitalManagementSystem
{
	public abstract class MyUser
	{
		public int Id { get; set; }

		public string Password { get; set; }

		public string Firstname { get; set; }

		public string Lastname { get; set; }

		public Address? Address { get; set; }

		public string? Email { get; set; }

		public string? Phone { get; set; }

		public override abstract string ToString();

		public abstract void DisplayMenu();

		public string GetFullName()
		{
			return $"{Firstname} {Lastname}";
		}
	}

}