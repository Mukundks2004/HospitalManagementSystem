namespace HospitalManagementSystem
{
	public class Patient(
		string id,
		string password,
		string firstname,
		string lastname,
		Address address,
		string email,
		string phone) : User(id, password, firstname, lastname, address, email, phone)
	{
		public Doctor? Doctor { get; set; } = null;
		public override void DisplayMenu()
		{
			Utilities.PrintMessageInBox("Patient Menu");
			Console.WriteLine(@$"Welcome to DOTNET Hospital Management System {Firstname} {Lastname}

Please choose an option:
1. List patient details
2. List my doctor details
3. List all appointments
4. Book appointment
5. Exit to login
6. Exit system");
			var choice = Console.ReadKey().Key;
			switch (choice)
			{
				case ConsoleKey.D1:
					Console.WriteLine();
					//Remove ^^ when doing console stuff
					ViewMyDetails();
					break;
				case ConsoleKey.D2:
					Console.WriteLine();
					//Remove ^^ when doing console stuff
					ViewMyDoctor();
					break;
			}
		}

		void ViewMyDetails()
		{
			Utilities.PrintMessageInBox("My Details");
			PrintDetailsHeader();
			Console.WriteLine(this);
		}

		void ViewMyDoctor()
		{
			if (Doctor is not null)
			{
				Utilities.PrintMessageInBox("My Doctor");
				Doctor.PrintDetailsHeader();
				Console.WriteLine(Doctor);
			}
		}

		public override string ToString()
		{
			var name = $"{Firstname} {Lastname}";
			var doctorName = Doctor is null ? string.Empty : $"{Doctor.Firstname} {Doctor.Lastname}";
			return $"{Id, -6}{Constants.VerticalLine} {name, -19}{Constants.VerticalLine} {doctorName, -19}{Constants.VerticalLine} {Email, -19}{Constants.VerticalLine} {Phone, -11}{Constants.VerticalLine} {Address}";
		}

		public static void PrintDetailsHeader()
		{
			Console.WriteLine($"ID{new string(' ', 4)}{Constants.VerticalLine} Patient Name{new string(' ', 7)}{Constants.VerticalLine} Doctor{new string(' ', 13)}{Constants.VerticalLine} Email Address{new string(' ', 6)}{Constants.VerticalLine} Phone{new string(' ', 6)}{Constants.VerticalLine} Address");
			Console.WriteLine($"{new string(Constants.HorizontalLine, 6)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 12)}{Constants.Center}{new string(Constants.HorizontalLine, 35)}");
		}
	}
}