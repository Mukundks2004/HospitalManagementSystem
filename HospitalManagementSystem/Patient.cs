namespace HospitalManagementSystem
{
	public class Patient : HospitalUser 
	{
		public ICollection<Appointment>? Appointments { get; set; }

		public int? DoctorId { get; set; }

		public Doctor? Doctor { get; set; }
	}
}