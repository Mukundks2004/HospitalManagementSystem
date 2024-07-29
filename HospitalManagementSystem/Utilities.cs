namespace HospitalManagementSystem
{
	public class Utilities
	{
		public static IdGenerator AdminIdGenerator { get; } = new(1000);

		public static IdGenerator DoctorIdGenerator { get; } = new(2000);

		public static IdGenerator PatientIdGenerator { get; } = new(3000);

		public static IdGenerator AppointmentIdGenerator { get; } = new(4000);

		public static IdGenerator AddressIdGenerator { get; } = new(5000);

		/// <summary>
		/// Prints a message in a box at the top of the screen
		/// </summary>
		/// <param name="message">Message to include in box. Must be fewer than 38 characters.</param>
		public static void PrintMessageInBox(string message)
		{
			var width = 2 * Constants.BoxPadding + Constants.BoxHeader.Length;
			var line = new string(Constants.HorizontalLine, width);

			Console.WriteLine($"{Constants.TopLeft}{line}{Constants.TopRight}");

			Console.WriteLine($"{Constants.VerticalLine}{new string(' ', Constants.BoxPadding)}{Constants.BoxHeader}{new string(' ', Constants.BoxPadding)}{Constants.VerticalLine}");
			Console.WriteLine($"{Constants.MiddleLeft}{line}{Constants.MiddleRight}");
			Console.WriteLine($"{Constants.VerticalLine}{new string(' ', (width - message.Length) / 2)}{message}{new string(' ', (width - message.Length + 1) / 2)}{Constants.VerticalLine}");

			Console.WriteLine($"{Constants.BottomLeft}{line}{Constants.BottomRight}");
		}

		/// <summary>
		/// Prompts the user with the provided prompt, then censors their password with '*'
		/// </summary>
		/// <param name="prompt"></param>
		/// <returns></returns>
		public static string ReadPassword(string prompt = "Password: ")
		{
			var pass = string.Empty;
			ConsoleKey key;
			Console.Write(prompt);
			do
			{
				var keyInfo = Console.ReadKey(intercept: true);
				key = keyInfo.Key;

				if (key == ConsoleKey.Backspace && pass.Length > 0)
				{
					Console.Write("\b \b");
					pass = pass[0..^1];
				}
				else if (!char.IsControl(keyInfo.KeyChar))
				{
					Console.Write("*");
					pass += keyInfo.KeyChar;
				}
			} while (key != ConsoleKey.Enter);
			Console.Write("\n");
			return pass;
		}

		/// <summary>
		/// Reads input from the user and returns an empty string if the user ctrl-Zs
		/// </summary>
		/// <param name="prompt"></param>
		/// <returns></returns>
		public static string ReadLine(string prompt = "")
		{
			Console.Write(prompt);
			return Console.ReadLine() ?? string.Empty;
		}
	}
}
