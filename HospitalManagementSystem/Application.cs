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
		readonly HospitalService _hospitalService;

		string _feedback = string.Empty;

		User? CurrentUser { get; set; }

		AppState State { get; set; } = AppState.Login;

		Utilities Utilities { get; set; } = new();

		public Application(HospitalService hospitalService)
		{
			_hospitalService = hospitalService;
		}

		/// <summary>
		/// Runs the login process until the application state is set to exit
		/// </summary>
		public void Run()
		{
			_hospitalService.AddSeedData();

			while (State != AppState.Exit)
			{
				Console.Clear();
				if (_feedback != string.Empty)
				{
					Console.WriteLine($"{_feedback}\n");
					_feedback = string.Empty;
				}

				switch (State)
				{
					case AppState.Login:
						Login();
						break;
					case AppState.Menu:
						State = _hospitalService.DisplayMenu(CurrentUser!); 
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
			else
			{
				_feedback = "Incorrect login details, please try again";
			}
		}

		bool Authenticate(string id, string password)
		{
			try
			{
				CurrentUser = _hospitalService.GetAllUsers().Where(u => u.Id.ToString() == id && u.Password == password).FirstOrDefault();
				return CurrentUser is not null;
			}
			catch (Exception)
			{
				throw new HospitalManagementSystemException("Authentication failed due to an internal error.");
			}
		}

		void AddPatient()
		{
			Utilities.PrintMessageInBox("Add Patient");
			Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");
		}
	}
}