namespace HospitalManagementSystem
{
	public class DoctorService
	{
		readonly UserRepository _userRepository;
		readonly AppointmentRepository _appointmentRepository;
		readonly AddressRepository _addressRepository;

		string _feedback = string.Empty;

		public DoctorService(UserRepository userRepository, AppointmentRepository appointmentRepository, AddressRepository addressRepository)
		{
			_userRepository = userRepository;
			_appointmentRepository = appointmentRepository;
			_addressRepository = addressRepository;
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
	}
}
