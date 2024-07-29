namespace HospitalManagementSystem
{
	public static class AppointmentExtensions
	{
		public static string GetString(this Appointment appointment)
		{
			return $"{appointment.Id, -6}{Constants.VerticalLine} {appointment.Doctor.GetFullName(), -19}{Constants.VerticalLine} {appointment.Patient.GetFullName(), -19}{Constants.VerticalLine} {appointment.Description}";
		}

		public static void PrintDetailsHeader()
		{
			Console.WriteLine($"ID{new string(' ', 4)}{Constants.VerticalLine} Doctor{new string(' ', 13)}{Constants.VerticalLine} Patient{new string(' ', 12)}{Constants.VerticalLine} Description");
			Console.WriteLine($"{new string(Constants.HorizontalLine, 6)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 35)}");
		}
	}
}