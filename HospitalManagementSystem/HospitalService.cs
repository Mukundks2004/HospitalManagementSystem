using System.Net.Mail;
using System.Net;
using System.ComponentModel.DataAnnotations;

namespace HospitalManagementSystem
{
	public class HospitalService
	{
		readonly UserRepository _userRepository;
		readonly AppointmentRepository _appointmentRepository;
		readonly AddressRepository _addressRepository;
		readonly DbInitializer _dbInitializer;

		readonly PatientService _patientService;
		readonly DoctorService _doctorService;

		string _feedback = string.Empty;

		public HospitalService(UserRepository userRepository, AppointmentRepository appointmentRepository, AddressRepository addressRepository, DbInitializer dbInitializer, DoctorService doctorService, PatientService patientService)
		{
			_userRepository = userRepository;
			_appointmentRepository = appointmentRepository;
			_addressRepository = addressRepository;

			_patientService = patientService;
			_doctorService = doctorService;

			_dbInitializer = dbInitializer;
		}

		/// <summary>
		/// Populates DB with doctors, patients, admins, addresses and appointments
		/// </summary>
		public void AddSeedData()
		{
			_dbInitializer.DatabaseRefresh();

			var sampleAddresses = SeedData.GetSampleAddresses();
			var addressIds = sampleAddresses.Select(a => a.Id).ToArray();
			_addressRepository.AddRange(sampleAddresses);

			var samplePatients = SeedData.GetSamplePatients(addressIds);
			_userRepository.AddRange(samplePatients);

			var sampleDoctors = SeedData.GetSampleDoctors(addressIds);
			_userRepository.AddRange(sampleDoctors);

			_userRepository.AddRange(SeedData.GetSampleAdmin());
			_appointmentRepository.AddRange(SeedData.GetSampleAppointments(samplePatients.Select(a => a.Id).ToArray(), sampleDoctors.Select(a => a.Id).ToArray()));

			_userRepository.SaveChanges();
			_addressRepository.SaveChanges();
			_appointmentRepository.SaveChanges();
		}

		/// <summary>
		/// Returns all users including addresses for hospital users
		/// </summary>
		/// <returns></returns>
		public IEnumerable<User> GetAllUsers()
		{
			var hospitalUsers = _userRepository.GetAllHospitalUsersWithAddress().ToList<User>();
			var admin = _userRepository.GetAllAdmin().ToList<User>();
			return hospitalUsers.Concat(admin);
		}

		/// <summary>
		/// Displays the appropriate menu options depending on the user type
		/// </summary>
		/// <param name="user"></param>
		/// <returns></returns>
		public AppState DisplayMenu(User user)
		{
			Console.Clear();
			if (_feedback != string.Empty)
			{
				Console.WriteLine($"{_feedback}\n");
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

		/// <summary>
		/// Displays the menu for the logged in admin, and allows the admin to complete an operation.
		/// Returns the new application state following menu display.
		/// </summary>
		/// <param name="admin"></param>
		/// <returns></returns>
		AppState DisplayAdminMenu(Admin admin)
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

			var choice = Console.ReadKey().Key;
			switch (choice)
			{
				case ConsoleKey.D1:
					_doctorService.ListAllDoctors();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D2:
					_doctorService.CheckParticularDoctor();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D3:
					_patientService.ListAllPatients();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D4:
					_patientService.CheckParticularPatient();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D5:
					_doctorService.AddDoctor();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D6:
					_patientService.AddPatient();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D7:
					return AppState.Login;
				case ConsoleKey.D8:
					return AppState.Exit;
				default:
					_feedback = "Sorry that's an invalid choice!";
					break;
			}

			return AppState.Menu;
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
					_patientService.BookAppointment(patient);
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
					_patientService.CheckParticularPatient();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D5:
					_doctorService.AppointmentsWith(doctor);
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
	}
}