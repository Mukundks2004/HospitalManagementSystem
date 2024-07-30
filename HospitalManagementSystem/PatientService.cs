using System.ComponentModel.DataAnnotations;
using System.Net.Mail;
using System.Net;

namespace HospitalManagementSystem
{
	public class PatientService
	{
		readonly UserRepository _userRepository;
		readonly AppointmentRepository _appointmentRepository;
		readonly AddressRepository _addressRepository;

		string _feedback = string.Empty;

		public PatientService(UserRepository userRepository, AppointmentRepository appointmentRepository, AddressRepository addressRepository)
		{
			_userRepository = userRepository;
			_appointmentRepository = appointmentRepository;
			_addressRepository = addressRepository;
		}

		/// <summary>
		/// Prompts the user for an appointment description.
		/// Creates a new appointment with the patient and the doctor.
		/// Sets a doctor if no doctor has been set.
		/// Sends a confirmation email when done.
		/// </summary>
		/// <param name="patient"></param>
		public void BookAppointment(Patient patient)
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
				SendMailConfirmingAppointment(patient.Email!, appointmentDescription, chosenDoctor, patient);
				Console.WriteLine("Email sent successfully!");
			}
			catch (Exception)
			{
				Console.WriteLine("Email unfortunately did not send.");
			}
		}

		/// <summary>
		/// Prompts users until valid ID given and displays details of patient matching ID
		/// </summary>
		public void CheckParticularPatient()
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

		/// <summary>
		/// Lists all patients in DB in table format
		/// </summary>
		public void ListAllPatients()
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

		/// <summary>
		/// Prompts the user for patient information and adds a new patient to the DB
		/// </summary>
		public void AddPatient()
		{
			while (true)
			{
				Console.Clear();
				if (_feedback != string.Empty)
				{
					Console.WriteLine($"{_feedback}\n");
					_feedback = string.Empty;
				}

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

				var emailValidator = new EmailAddressAttribute();
				if (!emailValidator.IsValid(email))
				{
					_feedback = "email is not valid, please try again.";
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
				break;
			}
		}

		/// <summary>
		/// Sends an email from the postman account to the provided email, including the appointment just created.
		/// </summary>
		/// <param name="email"></param>
		/// <param name="doctor"></param>
		/// <param name="patient"></param>
		/// <exception cref="HospitalManagementSystemException"></exception>
		public void SendMailConfirmingAppointment(string email, string description, Doctor doctor, Patient patient)
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
				Body = $"You, {patient.GetFullName()}, have just booked an appointment with doctor {doctor.GetFullName()}. The appointment is about \"{description}\".",
			};

			mailMessage.To.Add(email);

			client.Send(mailMessage);
		}
	}
}
