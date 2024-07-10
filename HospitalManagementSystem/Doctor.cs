#nullable enable

namespace HospitalManagementSystem
{
	public class Doctor(
		int id,
		string password,
		string firstname,
		string lastname,
		Address address,
		string email,
		string phone) : User(id, password, firstname, lastname, address, email, phone)
	{
		public override void DisplayMenu()
		{
			Utilities.PrintMessageInBox("Doctor Menu");
			Console.WriteLine(@$"Welcome to DOTNET Hospital Management System {GetFullName()}
			
Please choose an option:
1. List doctor details
2. List patients
3. List appointments
4. Check particular patient
5. List appointments with patient
6. Logout
7. Exit");
		}

		public override string ToString()
		{
			return $"{Id, -6}{Constants.VerticalLine} {GetFullName(), -19}{Constants.VerticalLine} {Email, -19}{Constants.VerticalLine} {Phone, -11}{Constants.VerticalLine} {Address}";
		}

		public static void PrintDetailsHeader()
		{
			Console.WriteLine($"ID{new string(' ', 4)}{Constants.VerticalLine} Name{new string(' ', 15)}{Constants.VerticalLine} Email Address{new string(' ', 6)}{Constants.VerticalLine} Phone{new string(' ', 6)}{Constants.VerticalLine} Address");
			Console.WriteLine($"{new string(Constants.HorizontalLine, 6)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 12)}{Constants.Center}{new string(Constants.HorizontalLine, 35)}");
		}
	}
}