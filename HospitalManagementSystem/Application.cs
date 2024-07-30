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

		/// <summary>
		/// Prompts the user for their details and changes app state and feedback if details are correct/incorrect.
		/// </summary>
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

		/// <summary>
		/// Returns true if a user can be found in the DB with credentials matching those provided
		/// Updates current user
		/// </summary>
		/// <param name="id"></param>
		/// <param name="password"></param>
		/// <returns></returns>
		/// <exception cref="HospitalManagementSystemException"></exception>
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
	}
}