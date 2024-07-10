namespace HospitalManagementSystem
{
	public class Utilities
	{
		//Might make a whole class of these, use for doctor, patient, apppointment, etc.
		int _currentId = 1000;

		public int CurrentId
		{
			get => _currentId++;
		}

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

		public static string ReadLine(string prompt = "")
		{
			Console.Write(prompt);
			return Console.ReadLine() ?? string.Empty;
		}
	}
}
