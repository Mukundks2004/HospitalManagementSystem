namespace HospitalManagementSystem
{
	public class Doctor : HospitalUser 
	{
		public ICollection<Appointment>? Appointments { get; set; }

		public ICollection<Patient>? Patients { get; set; }

		/// <summary>
		/// Returns a string representation of a doctor
		/// </summary>
		/// <param name="patient"></param>
		/// <returns></returns>
		public override string ToString()
		{
			return $"{Id,-6}{Constants.VerticalLine} {this.GetFullName(),-19}{Constants.VerticalLine} {Email,-19}{Constants.VerticalLine} {Phone,-11}{Constants.VerticalLine} {Address}";
		}
	}
}