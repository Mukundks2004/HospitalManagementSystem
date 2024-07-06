namespace HospitalManagementSystem
{
	public enum AppState
	{
		Login,
		Menu,
		Exit
	}

	public class Application
	{
		User? CurrentUser { get; set; }
		AppState State { get; set; } = AppState.Login;
		Utilities Utilities { get; set; } = new();

		public void Run()
		{
			while (State != AppState.Exit)
			{
				switch (State)
				{
					case AppState.Login:
						Login();
						break;
					case AppState.Menu:
						CurrentUser.DisplayMenu(); 
						break;
				}
			}
        }

		void Login()
		{
			Utilities.PrintMessageInBox("Login");
			Console.WriteLine();
			var id = Utilities.ReadLine("ID: ");
			var password = Utilities.ReadPassword();

			if (Authenticate(id, password))
			{
				State = AppState.Menu;
			}
		}

		bool Authenticate(string username, string password)
		{
			try
			{
				CurrentUser = GetUserFromDatabase(username, password);
				return CurrentUser is not null;
			}
			catch (Exception)
			{
				throw new HospitalManagementSystemException("Authentication failed due to an internal error.");
			}
		}

		private void PatientMenu()
		{
			var choice = Console.ReadKey();
			switch (choice.Key)
			{
				case ConsoleKey.D1:
					CurrentUser.ToString();
					break;
				case ConsoleKey.D2:
				case ConsoleKey.D3:
				case ConsoleKey.D4:
				case ConsoleKey.D5:
					State = AppState.Login;
					break;
				case ConsoleKey.D6:
					State = AppState.Exit;
					break;
			}
		}

		private void AddPatient()
		{
			Utilities.PrintMessageInBox("Add Patient");
			Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");
		}

		//might need to make the below static later
		User? GetUserFromDatabase(string id, string password)
		{
			if (!Constants.TempDetails.TryGetValue(id, out string? value))
			{
				return null;
			}
			if (!value.Equals(password))
			{
				return null;
			}
			//return new Admin("a", "b", "g", "pp");
			var x = new Patient(Utilities.CurrentId.ToString("0000"), "b", "g", "pp", new Address("a", "b", "c", "d", "e"), "@", "0123");
			var y = new Doctor(Utilities.CurrentId.ToString("0000"), "skjdjksf", "sfdlkklds", "pp", new Address("a", "b", "c", "d", "eoooo"), "@", "0123");
			x.Doctor = y;
			return x;
		}
	}
}