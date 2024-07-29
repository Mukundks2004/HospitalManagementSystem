namespace HospitalManagementSystem
{
	public class Patient : HospitalUser 
	{
		public ICollection<Appointment>? Appointments { get; set; }

		public int? DoctorId { get; set; } = null;

		public Doctor? Doctor { get; set; } = null;
	}
}