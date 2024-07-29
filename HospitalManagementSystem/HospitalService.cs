using System.Numerics;

namespace HospitalManagementSystem
{
	public class HospitalService
	{
		readonly UserRepository _userRepository;
		readonly AppointmentRepository _appointmentRepository;
		readonly AddressRepository _addressRepository;

		string _feedback = string.Empty;

		public HospitalService(UserRepository userRepository, AppointmentRepository appointmentRepository, AddressRepository addressRepository)
		{
			_userRepository = userRepository;
			_appointmentRepository = appointmentRepository;
			_addressRepository = addressRepository;
		}

		public void AddSeedData()
		{
			var sampleAddresses = SeedData.GetSampleAddresses();
			var addressIds = sampleAddresses.Select(a => a.Id).ToArray();
			_addressRepository.AddRange(sampleAddresses);

			var samplePatients = SeedData.GetSamplePatients(addressIds);
			_userRepository.AddRange(samplePatients);

			var sampleDoctors = SeedData.GetSampleDoctors(addressIds);
			_userRepository.AddRange(sampleDoctors);

			_userRepository.AddRange(SeedData.GetSampleAdmins());
			_appointmentRepository.AddRange(SeedData.GetSampleAppointments(samplePatients.Select(a => a.Id).ToArray(), sampleDoctors.Select(a => a.Id).ToArray()));

			_userRepository.SaveChanges();
			_addressRepository.SaveChanges();
			_appointmentRepository.SaveChanges();
		}

		public IEnumerable<User> GetAllUsers()
		{
			return _userRepository.GetAll();
		}

		public AppState DisplayMenu(User user)
		{
			Console.Clear();
			if (_feedback != string.Empty)
			{
				Console.WriteLine(_feedback);
				Console.WriteLine();
				_feedback = string.Empty;
			}

			AppState result = AppState.Login;
			switch (user)
			{
				case Doctor doctor:
					result = DisplayDoctorMenu(doctor);
					break;
				case Patient patient:
					result = DisplayPatientMenu(patient);
					break;
				case Admin admin:
					result = DisplayAdminMenu(admin);
					break;
			}

			return result;
		}

		static AppState DisplayAdminMenu(Admin admin)
		{
			Utilities.PrintMessageInBox("Administrator Menu");
			Console.WriteLine(@$"Welcome to DOTNET Hospital Management System {admin.GetFullName()}

Please choose an option:
1. List all doctors
2. Check doctor details
3. List all patients
4. Check patient details
5. Add doctor
6. Add patient
7. Logout
8. Exit");
			return AppState.Exit;
		}

		AppState DisplayPatientMenu(Patient patient)
		{
			Utilities.PrintMessageInBox("Patient Menu");
			Console.WriteLine(@$"Welcome to DOTNET Hospital Management System {patient.GetFullName()}

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
					patient.ViewMyDetails();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D2:
					patient.ViewMyDoctor();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D3:
					patient.ViewMyAppointments();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D4:
					BookAppointment(patient);
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D5:
					return AppState.Login;
				case ConsoleKey.D6:
					return AppState.Exit;
				default:
					_feedback = "Sorry that's an invalid choice!";
					break;
			}

			return AppState.Menu;
		}

		AppState DisplayDoctorMenu(Doctor doctor)
		{
			Console.Clear();
			Utilities.PrintMessageInBox("Doctor Menu");
			Console.WriteLine(@$"Welcome to DOTNET Hospital Management System {doctor.GetFullName()}
			
Please choose an option:
1. List doctor details
2. List patients
3. List appointments
4. Check particular patient
5. List appointments with patient
6. Logout
7. Exit");
			var choice = Console.ReadKey().Key;
			switch (choice)
			{
				case ConsoleKey.D1:
					doctor.ViewMyDetails();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D2:
					doctor.ListPatients();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D3:
					doctor.ViewMyAppointments();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D4:
					CheckParticularPatient();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D5:
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D6:
					return AppState.Login;
				case ConsoleKey.D7:
					return AppState.Exit;
				default:
					_feedback = "Sorry that's an invalid choice!";
					break;
			}

			return AppState.Menu;
		}

		void BookAppointment(Patient patient)
		{
			Console.Clear();
			var chosenDoctor = patient.Doctor;
			if (chosenDoctor is null)
			{
				while (true)
				{
					Console.Clear();
					if (_feedback != string.Empty)
					{
						Console.WriteLine(_feedback);
						Console.WriteLine();
						_feedback = string.Empty;
					}

					Utilities.PrintMessageInBox("My Appointments");
					Console.WriteLine("You are not registered with any doctor! Please choose which doctor you would like to register with");

					var doctors = _userRepository.GetAllDoctors();
					var doctorNumber = 1;
					var totalDoctors = doctors.Count();

					foreach (var doctor in doctors)
					{
						Console.WriteLine($"{doctorNumber++}- {doctor.GetString()}");
					}

					var choice = Utilities.ReadLine("Please choose a doctor:\n");
					if (!int.TryParse(choice, out var choiceInt))
					{
						_feedback = "That's not a valid doctor!";
						continue;
					}

					if (choiceInt > totalDoctors || choiceInt < 1)
					{
						_feedback = "That's not a valid doctor!";
						continue;
					}

					chosenDoctor = doctors.ElementAt(choiceInt - 1);
					patient.DoctorId = chosenDoctor.Id;
					_userRepository.SaveChanges();
					break;
				}
			}
			
			Console.WriteLine($"You are booking a new appointment with {chosenDoctor.GetFullName()}");
			var appointmentDescription = Utilities.ReadLine("Description of the appointment: ");
			Console.WriteLine("The appointment has been booked successfully");

			var newAppointment = new Appointment() { DoctorId = chosenDoctor.Id, PatientId = patient.Id, Description = appointmentDescription };
			_appointmentRepository.Add(newAppointment);
			_appointmentRepository.SaveChanges();
		}

		void CheckParticularPatient()
		{
			while (true)
			{
				Console.Clear();
				if (_feedback != string.Empty)
				{
					Console.WriteLine(_feedback);
					Console.WriteLine();
					_feedback = string.Empty;
				}

				Utilities.PrintMessageInBox("Check Patient Details");
				var patientId = Utilities.ReadLine("\nEnter the ID of the patient to check: ");
				if (!int.TryParse(patientId, out var patientNumber))
				{
					_feedback = "Bad ID! Try again.";
					continue;
				}

				var patient = _userRepository.GetPatientById(patientNumber);
				if (patient is null)
				{
					_feedback = "Bad ID! Try again.";
					continue;
				}

				UserExtensions.PrintPatientDetailsHeader();
				Console.WriteLine(patient.GetString());
				break;
			}
		}
	}
}