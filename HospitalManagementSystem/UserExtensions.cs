namespace HospitalManagementSystem
{
	public static class UserExtensions
	{
		/// <summary>
		/// Returns the concatenated first and last names of a user
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public static string GetFullName(this User user)
		{
			return $"{user.Firstname} {user.Lastname}";
		}

		/// <summary>
		/// Displays the user's public fields
		/// </summary>
		/// <param name="patient"></param>
		public static void ViewMyDetails(this Patient patient)
		{
			Console.Clear();
			Utilities.PrintMessageInBox("My Details");
			Console.WriteLine($"\n{patient.GetFullName()}'s Details\n");
			PrintPatientDetailsHeader();
			Console.WriteLine(patient.GetString());
		}

		/// <summary>
		/// Displays the doctor's public fields
		/// </summary>
		/// <param name="doctor"></param>
		public static void ViewMyDetails(this Doctor doctor)
		{
			Console.Clear();
			Utilities.PrintMessageInBox("My Details");
			Console.WriteLine();
			PrintDoctorDetailsHeader();
			Console.WriteLine(doctor.GetString());
		}

		/// <summary>
		/// Displays the doctor belonging to a patient, and gives an explanation if one does not exist.
		/// </summary>
		/// <param name="patient"></param>
		public static void ViewMyDoctor(this Patient patient)
		{
			Console.Clear();
			var doctor = patient.Doctor;
			if (doctor is not null)
			{
				Utilities.PrintMessageInBox("My Doctor");
				Console.WriteLine($"\nYour doctor:\n");
				PrintDoctorDetailsHeader();
				Console.WriteLine(doctor.GetString());
			}
			else
			{
				Console.WriteLine("You don't have a doctor! Book an appointment to register with one!");
			}
		}

		/// <summary>
		/// Displays all appointments a patient is part of
		/// </summary>
		/// <param name="patient"></param>
		public static void ViewMyAppointments(this Patient patient)
		{
			Console.Clear();
			var appointments = patient.Appointments ?? [];
			Utilities.PrintMessageInBox("My Appointments");
			Console.WriteLine($"\nAppointments for {patient.GetFullName()}\n");
			AppointmentExtensions.PrintDetailsHeader();
			foreach (var appointment in appointments)
			{
				Console.WriteLine(appointment.GetString());
			}
		}

		/// <summary>
		/// Displays all patients who consider their doctor to be this doctor.
		/// </summary>
		/// <param name="doctor"></param>
		public static void ListPatients(this Doctor doctor)
		{
			Console.Clear();
			Utilities.PrintMessageInBox("My Patients");
			Console.WriteLine($"\nPatients assigned to {doctor.GetFullName()}:\n");
			PrintPatientDetailsHeader();
			foreach (var patient in doctor.Patients ?? [])
			{
				Console.WriteLine(patient.GetString());
			}
		}

		/// <summary>
		/// Displays all appointments this doctor has.
		/// </summary>
		/// <param name="doctor"></param>
		public static void ViewMyAppointments(this Doctor doctor)
		{
			Console.Clear();
			var appointments = doctor.Appointments ?? [];
			Utilities.PrintMessageInBox("All Appointments");
			Console.WriteLine();
			AppointmentExtensions.PrintDetailsHeader();
			foreach (var appointment in appointments)
			{
				Console.WriteLine(appointment.GetString());
			}
		}

		/// <summary>
		/// Prints the table header to display multiple patients
		/// </summary>
		public static void PrintPatientDetailsHeader()
		{
			Console.WriteLine($"ID{new string(' ', 4)}{Constants.VerticalLine} Name{new string(' ', 15)}{Constants.VerticalLine} Doctor{new string(' ', 13)}{Constants.VerticalLine} Email Address{new string(' ', 6)}{Constants.VerticalLine} Phone{new string(' ', 6)}{Constants.VerticalLine} Address");
			Console.WriteLine($"{new string(Constants.HorizontalLine, 6)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 12)}{Constants.Center}{new string(Constants.HorizontalLine, 35)}");
		}

		/// <summary>
		/// Prints the table header to display multiple doctors
		/// </summary>
		public static void PrintDoctorDetailsHeader()
		{
			Console.WriteLine($"ID{new string(' ', 4)}{Constants.VerticalLine} Name{new string(' ', 15)}{Constants.VerticalLine} Email Address{new string(' ', 6)}{Constants.VerticalLine} Phone{new string(' ', 6)}{Constants.VerticalLine} Address");
			Console.WriteLine($"{new string(Constants.HorizontalLine, 6)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 12)}{Constants.Center}{new string(Constants.HorizontalLine, 35)}");
		}

		/// <summary>
		/// Returns the string representation of a hospital user by calling the appropriate method
		/// </summary>
		/// <param name="hospitalUser"></param>
		/// <returns></returns>
		public static string GetString(this HospitalUser hospitalUser)
		{
			return hospitalUser switch
			{
				Doctor doctor => doctor.GetString(),
				Patient patient => patient.GetString(),
				_ => string.Empty
			};
		}

		/// <summary>
		/// Returns a string representation of a patient
		/// </summary>
		/// <param name="patient"></param>
		/// <returns></returns>
		public static string GetString(this Patient patient)
		{
			var doctorName = patient.Doctor is null ? string.Empty : $"{patient.Doctor.GetFullName()}";
			return $"{patient.Id,-6}{Constants.VerticalLine} {patient.GetFullName(),-19}{Constants.VerticalLine} {doctorName,-19}{Constants.VerticalLine} {patient.Email,-19}{Constants.VerticalLine} {patient.Phone,-11}{Constants.VerticalLine} {patient.Address!.GetString()}";
		}

		/// <summary>
		/// Returns a string representation of a doctor
		/// </summary>
		/// <param name="patient"></param>
		/// <returns></returns>
		public static string GetString(this Doctor doctor)
		{
			return $"{doctor.Id,-6}{Constants.VerticalLine} {doctor.GetFullName(),-19}{Constants.VerticalLine} {doctor.Email,-19}{Constants.VerticalLine} {doctor.Phone,-11}{Constants.VerticalLine} {doctor.Address!.GetString()}";
		}
	}
}
