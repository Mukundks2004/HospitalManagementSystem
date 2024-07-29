namespace HospitalManagementSystem
{
	public class Doctor : HospitalUser 
	{
		public ICollection<Appointment>? Appointments { get; set; }

		public ICollection<Patient>? Patients { get; set; }
	}
}