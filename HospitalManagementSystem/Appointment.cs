namespace HospitalManagementSystem
{
	public class Appointment
	{
		public int? Id { get; set; }

		public int? DoctorId { get; set; }

		public int? PatientId { get; set; }

		public Doctor? Doctor { get; set; }

		public Patient? Patient { get; set; }

		public string? Description { get; set; }

		/// <summary>
		/// Returns a string representation of an appointment
		/// </summary>
		/// <param name="appointment"></param>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{Id,-6}{Constants.VerticalLine} {Doctor!.GetFullName(),-19}{Constants.VerticalLine} {Patient!.GetFullName(),-19}{Constants.VerticalLine} {Description}";
		}
	}
}