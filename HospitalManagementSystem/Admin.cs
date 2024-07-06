namespace HospitalManagementSystem
{
	public class Admin(
		string id,
		string password,
		string firstname,
		string lastname) : User(id, password, firstname, lastname, null, null, null)
	{
		public override void DisplayMenu()
		{

			Utilities.PrintMessageInBox("Administrator Menu");
			Console.WriteLine(@$"Welcome to DOTNET Hospital Management System {Firstname} {Lastname}

Please choose an option:
1. List all doctors
2. Check doctor details
3. List all patients
4. Check patient details
5. Add doctor
6. Add patient
7. Logout
8. Exit");
		}

		public override string ToString()
		{
			throw new NotImplementedException();
		}
	}
}