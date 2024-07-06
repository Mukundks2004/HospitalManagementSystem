namespace HospitalManagementSystem
{
	public class Appointment(Doctor doctor, Patient patient, string description)
	{
		public Doctor Doctor { get; set; } = doctor;
		public Patient Patient { get; set; } = patient;
		public string Description { get; set; } = description;
	}
}