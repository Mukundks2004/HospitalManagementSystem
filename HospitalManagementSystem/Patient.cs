using System.Data.Entity;

namespace HospitalManagementSystem
{
	//public class Patient(
	//	int id,
	//	string password,
	//	string firstname,
	//	string lastname,
	//	Address address,
	//	string email,
	//	string phone) : User(id, password, firstname, lastname, address, email, phone)
	//{
	public class Patient : MyUser
	{
		public Doctor? Doctor { get; set; } = null;
		public override void DisplayMenu()
		{
			Utilities.PrintMessageInBox("Patient Menu");
			//Console.WriteLine(@$"Welcome to DOTNET Hospital Management System {GetFullName()}
			Console.WriteLine(@$"Welcome to DOTNET Hospital Management System

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
				case ConsoleKey.D3:
					Console.WriteLine();
					//Remove ^^ when doing console stuff
					ViewMyAppointments();
					break;
				case ConsoleKey.D4:
				case ConsoleKey.D5:
				case ConsoleKey.D6:
					break;
			}
		}

		void ViewMyDetails()
		{
			Utilities.PrintMessageInBox("My Details");
			//Console.WriteLine($"\n{GetFullName()}'s Details\n");
			PrintDetailsHeader();
			Console.WriteLine(this);
		}

		void ViewMyDoctor()
		{
			if (Doctor is not null)
			{
				Utilities.PrintMessageInBox("My Doctor");
				Console.WriteLine($"\nYour doctor:\n");
				Doctor.PrintDetailsHeader();
				Console.WriteLine(Doctor);
			}
		}

		void ViewMyAppointments()
		{
			var appointments = Application.GetAllAppointmentsForPatientWithId(9);
			Utilities.PrintMessageInBox("My Appointments");
			//Console.WriteLine($"\nAppointments for {GetFullName()}\n");
			Appointment.PrintDetailsHeader();
			foreach (var appointment in appointments)
			{
				Console.WriteLine(appointment);
			}
		}

		void BookAppointment()
		{
			Utilities.PrintMessageInBox("My Appointments");
			Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");
			if (Doctor is null)
			{

			}
		}

		public override string ToString()
		{
			var doctorName = Doctor is null ? string.Empty : $"{Doctor.GetFullName()}";
			return "uwu";
			//return $"{Id, -6}{Constants.VerticalLine} {GetFullName(), -19}{Constants.VerticalLine} {doctorName, -19}{Constants.VerticalLine} {Email, -19}{Constants.VerticalLine} {Phone, -11}{Constants.VerticalLine} {Address}";
		}

		public static void PrintDetailsHeader()
		{
			Console.WriteLine($"ID{new string(' ', 4)}{Constants.VerticalLine} Name{new string(' ', 15)}{Constants.VerticalLine} Doctor{new string(' ', 13)}{Constants.VerticalLine} Email Address{new string(' ', 6)}{Constants.VerticalLine} Phone{new string(' ', 6)}{Constants.VerticalLine} Address");
			Console.WriteLine($"{new string(Constants.HorizontalLine, 6)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 12)}{Constants.Center}{new string(Constants.HorizontalLine, 35)}");
		}
	}
}