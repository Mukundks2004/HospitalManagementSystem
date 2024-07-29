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
	}
}