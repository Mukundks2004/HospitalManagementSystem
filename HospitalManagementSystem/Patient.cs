namespace HospitalManagementSystem
{
	public class Patient : HospitalUser 
	{
		public ICollection<Appointment>? Appointments { get; set; }

		public int? DoctorId { get; set; }

		public Doctor? Doctor { get; set; }

		/// <summary>
		/// Returns a string representation of a patient
		/// </summary>
		/// <param name="patient"></param>
		/// <returns></returns>
		public override string ToString()
		{
			var doctorName = Doctor is null ? string.Empty : $"{Doctor.GetFullName()}";
			return $"{Id,-6}{Constants.VerticalLine} {this.GetFullName(),-19}{Constants.VerticalLine} {doctorName,-19}{Constants.VerticalLine} {Email,-19}{Constants.VerticalLine} {Phone,-11}{Constants.VerticalLine} {Address}";
		}
	}
}