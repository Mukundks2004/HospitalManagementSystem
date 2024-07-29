using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem
{
	public abstract class User
	{
		[Key]
		public int? Id { get; set; }

		public string? Password { get; set; }

		public string? Firstname { get; set; }

		public string? Lastname { get; set; }
	}
}