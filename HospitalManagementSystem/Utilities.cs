namespace HospitalManagementSystem
{
	public class Utilities
	{
		public static IdGenerator AdminIdGenerator { get; set; } = new(10000);

		public static IdGenerator DoctorIdGenerator { get; set; } = new(20000);

		public static IdGenerator PatientIdGenerator { get; set; } = new(30000);

		public static IdGenerator AppointmentIdGenerator { get; set; } = new(40000);

		public static IdGenerator AddressIdGenerator { get; set; } = new(50000);

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
		/// Prompts the user, then returns the response.
		/// </summary>
		/// <param name="prompt"></param>
		/// <returns></returns>
		public static string ReadLine(string prompt)
		{
			Console.Write(prompt);
			return ReadLine();
		}

		/// <summary>
		/// Reads the input from the user and returns it.
		/// Returns an empty string if the user ctrl-Zs
		/// </summary>
		/// <param name="prompt"></param>
		/// <returns></returns>
		public static string ReadLine()
		{
			return Console.ReadLine() ?? string.Empty;
		}
	}
}
