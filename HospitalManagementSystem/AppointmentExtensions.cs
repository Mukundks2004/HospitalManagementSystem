namespace HospitalManagementSystem
{
	public static class AppointmentExtensions
	{
		/// <summary>
		/// Returns a string representation of an appointment
		/// </summary>
		/// <param name="appointment"></param>
		/// <returns></returns>
		public static string GetString(this Appointment appointment)
		{
			return $"{appointment.Id, -6}{Constants.VerticalLine} {appointment.Doctor.GetFullName(), -19}{Constants.VerticalLine} {appointment.Patient.GetFullName(), -19}{Constants.VerticalLine} {appointment.Description}";
		}

		/// <summary>
		/// Prints the header used to create a table of appointments
		/// </summary>
		public static void PrintDetailsHeader()
		{
			Console.WriteLine($"ID{new string(' ', 4)}{Constants.VerticalLine} Doctor{new string(' ', 13)}{Constants.VerticalLine} Patient{new string(' ', 12)}{Constants.VerticalLine} Description");
			Console.WriteLine($"{new string(Constants.HorizontalLine, 6)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 35)}");
		}
	}
}