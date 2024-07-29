namespace HospitalManagementSystem
{
	public static class SeedData
	{
		/*public static void Initialize(HospitalManagementSystemContext context)
		{
			/*var mksAddress = new Address() { Id = 1, Postcode = "0000", StreetName = "Fawcett", StreetNumber = "7", State = "NSW", Suburb = "Glenfield"};
			context.Addresses.Add(mksAddress);
			context.SaveChanges();

			//var mksPatient = new Patient() { Address = mksAddress, AddressId = 1, Firstname = "M", Lastname = "S", Email = "mk@", Phone = "040404", Id = 1, Password = "mks"};
			//var mksPatient = new Patient() { Address = mksAddress, Firstname = "M", Lastname = "S", Email = "mk@", Phone = "040404", Id = 1, Password = "mks"};
			var mksPatient = new Patient() { AddressId = 1, Firstname = "M", Lastname = "S", Email = "mk@", Phone = "040404", Id = 1, Password = "mks"};
			context.Users.Add(mksPatient);
			context.SaveChanges();

			var sampleAddresses = GetSampleAddresses();
			context.Addresses.AddRange(sampleAddresses);
			context.SaveChanges();

			var addressIds = context.Addresses.Select(a => a.Id).ToArray();

			var sampleDoctors = GetSampleDoctors(addressIds);
			context.Users.AddRange(sampleDoctors);
			context.SaveChanges();

			var samplePatients = GetSamplePatients(addressIds);
			context.Users.AddRange(samplePatients);
			context.SaveChanges();

			var sampleAdmins = GetSampleAdmins();
			context.Users.AddRange(sampleAdmins);
			context.SaveChanges();

			var patientIds = context.Users.OfType<Patient>().Select(a => a.Id).ToArray();
			var doctorIds = context.Users.OfType<Doctor>().Select(a => a.Id).ToArray();
			context.Appointments.AddRange(GetSampleAppointments(patientIds, doctorIds));
			context.SaveChanges();

			//var context2 = new HospitalManagementSystemContext();
			//var y = context2.Users.OfType<Patient>().Include(u => u.Address).FirstOrDefault();
			//var z = context.Users.OfType<Patient>().Include(u => u.Address).FirstOrDefault();
		}*/

		static Random rnd = new Random();

		public static Doctor[] GetSampleDoctors(int?[] addressIds)
		{
			return
			[
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "apple",
					Firstname = "Mukund",
					Lastname = "Srinivasan",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "mks@gmail.com",
					Phone = "0400000000"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "banana",
					Firstname = "Ananya",
					Lastname = "Srinivasan",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "ananya@gmail.com",
					Phone = "0400000001"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "cherry",
					Firstname = "Davey",
					Lastname = "Dyer",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "davey@gmail.com",
					Phone = "0400000002"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "onion",
					Firstname = "Sam",
					Lastname = "Rothwell",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "sammy@gmail.com",
					Phone = "0400000003"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "blueberry",
					Firstname = "Avinash",
					Lastname = "Singh",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "avi@gmail.com",
					Phone = "0400000004"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "tomato",
					Firstname = "Sophia",
					Lastname = "Nguyen",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "soph@gmail.com",
					Phone = "0400000005"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "turtle",
					Firstname = "Ethan",
					Lastname = "Mangan",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "e-man@gmail.com",
					Phone = "0400000006"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "table",
					Firstname = "Guhan",
					Lastname = "Sundar",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "da_fish@gmail.com",
					Phone = "0400000007"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "scarlett",
					Firstname = "Sally",
					Lastname = "Luu",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "seely@gmail.com",
					Phone = "0400000008"
				},
				new Doctor
				{
					Id = Utilities.DoctorIdGenerator.CurrentId,
					Password = "karate",
					Firstname = "David",
					Lastname = "Sorrell",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "phd@gmail.com",
					Phone = "0400000009"
				}
			];
		}

		public static Patient[] GetSamplePatients(int?[] addressIds)
		{
			return
			[
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "coconut",
					Firstname = "Joe",
					Lastname = "Biden",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "bigJ@gmail.com",
					Phone = "0400000010"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "rocks",
					Firstname = "Aiden",
					Lastname = "Gardner",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "rock_climber@gmail.com",
					Phone = "0400000011"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "bacon",
					Firstname = "Peppa",
					Lastname = "Pig",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "superman@gmail.com",
					Phone = "0400000012"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "donut",
					Firstname = "Josh",
					Lastname = "Roy",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "ultramarathon@gmail.com",
					Phone = "0400000013"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "lizard",
					Firstname = "Donald",
					Lastname = "Trump",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "orange@gmail.com",
					Phone = "0400000014"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "iphone",
					Firstname = "Jane",
					Lastname = "Citizen",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "jane@gmail.com",
					Phone = "0400000015"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "passionfruit",
					Firstname = "Sam",
					Lastname = "Sparks",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "reporter@gmail.com",
					Phone = "0400000016"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "tea",
					Firstname = "John",
					Lastname = "Wick",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "dog@gmail.com",
					Phone = "0400000017"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "chair",
					Firstname = "Liam",
					Lastname = "Neeson",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "taken@gmail.com",
					Phone = "0400000018"
				},
				new Patient
				{
					Id = Utilities.PatientIdGenerator.CurrentId,
					Password = "suitcase",
					Firstname = "Tom",
					Lastname = "Hanks",
					AddressId = addressIds[rnd.Next(addressIds.Length)],
					Email = "airport@gmail.com",
					Phone = "0400000019"
				},
			];
		}

		public static Admin[] GetSampleAdmins()
		{
			return
			[
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "admin",
					Firstname = "Test_Firstname",
					Lastname = "Test_Lastname",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "wintermelon",
					Firstname = "Joseph",
					Lastname = "Stalin",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "sword",
					Firstname = "Napoleon",
					Lastname = "Bonaparte",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "ship",
					Firstname = "James",
					Lastname = "Cook",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "america",
					Firstname = "Chris",
					Lastname = "Columbus",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "fig",
					Firstname = "George",
					Lastname = "Washington",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "cross",
					Firstname = "Jesus",
					Lastname = "Christ",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "lin",
					Firstname = "Alex",
					Lastname = "Hamilton",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "inventor",
					Firstname = "Leo",
					Lastname = "DaVinci",
				},
				new Admin
				{
					Id = Utilities.AdminIdGenerator.CurrentId,
					Password = "potato",
					Firstname = "Fidel",
					Lastname = "Castro",
				},
			];
		}

		public static Appointment[] GetSampleAppointments(int?[] patientIds, int?[] doctorIds)
		{
			var appointments = new Appointment[10];
			var descriptions = new string[]
			{
				"Bad sore throat",
				"Feeling shivery",
				"Slightly feverish",
				"Twisted ankle",
				"Terrible shin splints",
				"Headache",
				"Cramps in stomach",
				"Itchy brains",
				"Stunted fingernail growth",
				"Very drowsy during the day"
			};

			for (int i = 0; i < 10; i++)
			{
				appointments[i] = new Appointment
				{
					Id = Utilities.AppointmentIdGenerator.CurrentId,
					PatientId = patientIds[i],
					DoctorId = doctorIds[i],
					Description = descriptions[i],
				};
			}

			return appointments;
		}


		public static Address[] GetSampleAddresses() =>
			[
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "72",
					StreetName = "Barker Street",
					Suburb = "Woodanilling",
					Postcode = "6316",
					State = "WA"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "6",
					StreetName = "Bundeena Road",
					Suburb = "Woodbine",
					Postcode = "2560",
					State = "NSW"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "74",
					StreetName = "O'Riordan Road",
					Suburb = "Alexandria",
					Postcode = "2015",
					State = "NSW"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "15",
					StreetName = "Broadway",
					Suburb = "Ultimo",
					Postcode = "2007",
					State = "NSW"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "7",
					StreetName = "Kenwyn Street",
					Suburb = "Hurstville",
					Postcode = "2111",
					State = "NSW"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "34",
					StreetName = "Hill Street",
					Suburb = "Cabramatta",
					Postcode = "2344",
					State = "NSW"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "62",
					StreetName = "Railway Parade",
					Suburb = "Glenfield",
					Postcode = "2167",
					State = "NSW"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "1",
					StreetName = "Roy Watts Road",
					Suburb = "Glenfield",
					Postcode = "2167",
					State = "NSW"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "1",
					StreetName = "Powerhouse Road",
					Suburb = "Casula",
					Postcode = "2170",
					State = "NSW"
				},
				new Address
				{
					Id = Utilities.AddressIdGenerator.CurrentId,
					StreetNumber = "52",
					StreetName = "Scott Street",
					Suburb = "Liverpool",
					Postcode = "2170",
					State = "NSW"
				}
			];
	}
}
