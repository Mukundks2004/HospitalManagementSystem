using System.ComponentModel.DataAnnotations;

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

		/// <summary>
		/// Prompts users until valid ID given and displays details of doctor matching ID
		/// </summary>
		public void CheckParticularDoctor()
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
				Console.WriteLine("\nPlease enter the ID of the doctor whose details you are checking.\n");
				var doctorId = Utilities.ReadLine();
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

		/// <summary>
		/// Prompts user for patient ID.
		/// Returns all appointments doctor has with particular patient
		/// </summary>
		/// <param name="doctor"></param>
		public void AppointmentsWith(Doctor doctor)
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
					Console.WriteLine(appointment);
				}

				break;
			}
		}

		/// <summary>
		/// Lists all doctors in DB
		/// </summary>
		public void ListAllDoctors()
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

		/// <summary>
		/// Prompts user for details and adds doctor with details to DB
		/// </summary>
		public void AddDoctor()
		{
			while (true)
			{
				Console.Clear();
				if (_feedback != string.Empty)
				{
					Console.WriteLine($"{_feedback}\n");
					_feedback = string.Empty;
				}

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

				var emailValidator = new EmailAddressAttribute();
				if (!emailValidator.IsValid(email))
				{
					_feedback = "Email is not valid, try again please";
					continue;
				}

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
				break;
			}
		}
	}
}
