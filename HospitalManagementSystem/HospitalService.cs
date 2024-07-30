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

		string _feedback = string.Empty;

		public HospitalService(UserRepository userRepository, AppointmentRepository appointmentRepository, AddressRepository addressRepository, DbInitializer dbInitializer)
		{
			_userRepository = userRepository;
			_appointmentRepository = appointmentRepository;
			_addressRepository = addressRepository;
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
					ListAllDoctors();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D2:
					CheckParticularDoctor();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D3:
					ListAllPatients();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D4:
					CheckParticularPatient();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D5:
					AddDoctor();
					_ = Utilities.ReadLine("\nPress Enter to return to menu...");
					break;
				case ConsoleKey.D6:
					AddPatient();
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
					AppointmentsWith(doctor);
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
						Console.WriteLine($"{_feedback}\n");
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

			try
			{
				SendMailConfirmingAppointment(patient.Email, chosenDoctor, patient);
				Console.WriteLine("Email sent successfully!");
			}
			catch (Exception)
			{
				Console.WriteLine("Email unfortunately did not send.");
			}
		}

		void CheckParticularPatient()
		{
			while (true)
			{
				Console.Clear();
				if (_feedback != string.Empty)
				{
					Console.WriteLine($"{_feedback}\n");
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

				Console.WriteLine();
				UserExtensions.PrintPatientDetailsHeader();
				Console.WriteLine(patient.GetString());
				break;
			}
		}

		void CheckParticularDoctor()
		{
			while (true)
			{
				Console.Clear();
				if (_feedback != string.Empty)
				{
					Console.WriteLine($"{_feedback}\n");
					_feedback = string.Empty;
				}

				Utilities.PrintMessageInBox("Doctor Details");
				var doctorId = Utilities.ReadLine("\nPlease enter the ID of the doctor whose details you are checking.\n");
				if (!int.TryParse(doctorId, out var doctorNumber))
				{
					_feedback = "Bad ID! Try again.";
					continue;
				}

				var doctor = _userRepository.GetDoctorById(doctorNumber);
				if (doctor is null)
				{
					_feedback = "Bad ID! Try again.";
					continue;
				}

				Console.WriteLine($"\nDetails for {doctor.GetFullName()}\n");
				UserExtensions.PrintDoctorDetailsHeader();
				Console.WriteLine(doctor.GetString());
				break;
			}
		}

		void AppointmentsWith(Doctor doctor)
		{
			while (true)
			{
				Console.Clear();
				if (_feedback != string.Empty)
				{
					Console.WriteLine($"{_feedback}\n");
					_feedback = string.Empty;
				}

				Utilities.PrintMessageInBox("Appointments With");
				var patientId = Utilities.ReadLine("\nEnter the ID of the patient you would like to view appointments for: ");
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

				Console.WriteLine();
				AppointmentExtensions.PrintDetailsHeader();
				var appointments = _appointmentRepository.Find(a => a.DoctorId == doctor.Id && a.PatientId == patient.Id);
				foreach (var appointment in appointments)
				{
					Console.WriteLine(appointment.GetString());
				}

				break;
			}
		}

		void ListAllDoctors()
		{
			Console.Clear();
			Utilities.PrintMessageInBox("All Doctors");
			Console.WriteLine("\nAll doctors registered to the DOTNET Hospital Management System\n");
			UserExtensions.PrintDoctorDetailsHeader();
			foreach (var doctor in _userRepository.GetAllDoctors())
			{
				Console.WriteLine(doctor.GetString());
			}
		}

		void ListAllPatients()
		{
			Console.Clear();
			Utilities.PrintMessageInBox("All Patients");
			Console.WriteLine("\nAll patients registered to the DOTNET Hospital Management System\n");
			UserExtensions.PrintDoctorDetailsHeader();
			foreach (var doctor in _userRepository.GetAllPatients())
			{
				Console.WriteLine(doctor.GetString());
			}
		}

		void AddDoctor()
		{
			Console.Clear();
			Utilities.PrintMessageInBox("Add Doctor");
			Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System");

			var firstname = Utilities.ReadLine("First Name: ");
			var lastname = Utilities.ReadLine("Last Name: ");
			var email = Utilities.ReadLine("Email: ");
			var phone = Utilities.ReadLine("Phone: ");
			var password = Utilities.ReadLine("Password: ");

			var streetnumber = Utilities.ReadLine("Street Number: ");
			var street = Utilities.ReadLine("Street: ");
			var city = Utilities.ReadLine("City: ");
			var state = Utilities.ReadLine("State: ");
			var postcode = Utilities.ReadLine("Postcode: ");

			var address = new Address()
			{
				Id = Utilities.AddressIdGenerator.CurrentId,
				State = state,
				StreetName = street,
				StreetNumber = streetnumber,
				Suburb = city,
				Postcode = postcode
			};

			var doctor = new Doctor()
			{
				Id = Utilities.DoctorIdGenerator.CurrentId,
				Firstname = firstname,
				Lastname = lastname,
				Email = email,
				Phone = phone,
				Password = password,
				AddressId = address.Id
			};

			_addressRepository.Add(address);
			_userRepository.Add(doctor);
			_addressRepository.SaveChanges();
			_userRepository.SaveChanges();

			Console.WriteLine($"{firstname} {lastname} added to the system!");
		}

		void AddPatient()
		{
			Console.Clear();
			Utilities.PrintMessageInBox("Add Patient");
			Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System");

			var firstname = Utilities.ReadLine("First Name: ");
			var lastname = Utilities.ReadLine("Last Name: ");
			var email = Utilities.ReadLine("Email: ");
			var phone = Utilities.ReadLine("Phone: ");
			var password = Utilities.ReadLine("Password: ");

			var streetnumber = Utilities.ReadLine("Street Number: ");
			var street = Utilities.ReadLine("Street: ");
			var city = Utilities.ReadLine("City: ");
			var state = Utilities.ReadLine("State: ");
			var postcode = Utilities.ReadLine("Postcode: ");

			var address = new Address()
			{
				Id = Utilities.AddressIdGenerator.CurrentId,
				State = state,
				StreetName = street,
				StreetNumber = streetnumber,
				Suburb = city,
				Postcode = postcode
			};

			var patient = new Patient()
			{
				Id = Utilities.PatientIdGenerator.CurrentId,
				Firstname = firstname,
				Lastname = lastname,
				Email = email,
				Phone = phone,
				Password = password,
				AddressId = address.Id
			};

			_addressRepository.Add(address);
			_userRepository.Add(patient);
			_addressRepository.SaveChanges();
			_userRepository.SaveChanges();

			Console.WriteLine($"{firstname} {lastname} added to the system!");
		}

		void SendMailConfirmingAppointment(string email, Doctor doctor, Patient patient)
		{
			var emailAddressAttribute = new EmailAddressAttribute();
			if (!emailAddressAttribute.IsValid(email))
			{
				throw new HospitalManagementSystemException();
			}

			string fromEmail = "hospitalpostmanmukund@gmail.com";
			string password = "fngd tmij ewmf rsgs";

			SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
			{
				Credentials = new NetworkCredential(fromEmail, password),
				EnableSsl = true
			};

			MailMessage mailMessage = new MailMessage
			{
				From = new MailAddress(fromEmail),
				Subject = "Appointment booked!",
				Body = $"You, {patient.GetFullName()}, have just booked an appointment with doctor {doctor.GetFullName()}.",
			};

			mailMessage.To.Add(email);

			client.Send(mailMessage);
		}
	}
}