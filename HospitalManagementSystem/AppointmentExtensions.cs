namespace HospitalManagementSystem
{
	public static class AppointmentExtensions
	{
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