using HospitalManagementSystem;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HospitalManagementSystemTests
{
	public class UserRepositoryTests
	{
		Mock<HospitalManagementSystemContext>? _mockContext;
		DbSet<User>? _userSetObject;
		UserRepository? _userRepository;

		[SetUp]
		public void Setup()
		{
			var address = new Address()
			{
				Id = 1,
				StreetNumber = "123",
				StreetName = "Real Street",
				Suburb = "Liverpool",
				State = "NSW",
				Postcode = "2570"
			};

			var doctor = new Doctor()
			{
				Id = 1,
				Firstname = "Davey",
				Lastname = "Dyer",
				Phone = "0400000000",
				Email = "davey@gmail.com",
				Address = address,
			};

			var doctor2 = new Doctor()
			{
				Id = 3,
				Firstname = "neil",
				Lastname = "armstong",
				Phone = "0400009900",
				Email = "neil@gmail.com",
				Address = address,
			};

			var patient = new Patient()
			{
				Id = 2,
				Firstname = "Mr",
				Lastname = "Pat",
				Phone = "0400000001",
				Email = "patient@gmail.com",
				Address = address,
			};

			var patient2 = new Patient()
			{
				Id = 4,
				Firstname = "Arjun",
				Lastname = "T",
				Phone = "0405000001",
				Email = "crickit@gmail.com",
				Address = address,
			};

			var admin1 = new Admin()
			{
				Id = 5,
				Firstname = "Admin",
				Lastname = "Admin"
			};

			var admin2 = new Admin()
			{
				Id = 6,
				Firstname = "Tejas",
				Lastname = "IsCool"
			};

			_userSetObject = MockDbHelpers.GetQueryableMockDbSet(new List<User> { doctor, doctor2, patient, patient2, admin2, admin1 });
			_mockContext = new Mock<HospitalManagementSystemContext>();
			_mockContext.Setup(h => h.Users).Returns(_userSetObject);
			_mockContext.Setup(h => h.Set<User>()).Returns(_userSetObject);
			_userRepository = new UserRepository(_mockContext.Object);
		}

		[Test]
		public void TestGetDoctorById()
		{
			// Act
			var doctor = _userRepository!.GetDoctorById(1);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(doctor, Is.Not.Null);
				Assert.That(doctor!.Id, Is.EqualTo(1));
				Assert.That(doctor.Firstname, Is.EqualTo("Davey"));
				Assert.That(doctor.Lastname, Is.EqualTo("Dyer"));
				Assert.That(doctor.Phone, Is.EqualTo("0400000000"));
				Assert.That(doctor.Email, Is.EqualTo("davey@gmail.com"));

				Assert.That(doctor.Address!.Id, Is.EqualTo(1));
				Assert.That(doctor.Address.StreetName, Is.EqualTo("Real Street"));
				Assert.That(doctor.Address.StreetNumber, Is.EqualTo("123"));
				Assert.That(doctor.Address.State, Is.EqualTo("NSW"));
				Assert.That(doctor.Address.Suburb, Is.EqualTo("Liverpool"));
				Assert.That(doctor.Address.Postcode, Is.EqualTo("2570"));
			});
		}

		[Test]
		public void TestGetPatientById()
		{
			// Act
			var patient = _userRepository!.GetPatientById(2);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(patient, Is.Not.Null);
				Assert.That(patient!.Id, Is.EqualTo(2));
				Assert.That(patient.Firstname, Is.EqualTo("Mr"));
				Assert.That(patient.Lastname, Is.EqualTo("Pat"));
				Assert.That(patient.Phone, Is.EqualTo("0400000001"));
				Assert.That(patient.Email, Is.EqualTo("patient@gmail.com"));

				Assert.That(patient.Address!.Id, Is.EqualTo(1));
				Assert.That(patient.Address.StreetName, Is.EqualTo("Real Street"));
				Assert.That(patient.Address.StreetNumber, Is.EqualTo("123"));
				Assert.That(patient.Address.State, Is.EqualTo("NSW"));
				Assert.That(patient.Address.Suburb, Is.EqualTo("Liverpool"));
				Assert.That(patient.Address.Postcode, Is.EqualTo("2570"));
			});
		}

		[Test]
		public void TestGetAllDoctors()
		{
			// Act
			var doctors = _userRepository!.GetAllDoctors();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(doctors, Is.Not.Null);
				Assert.That(doctors.Count(), Is.EqualTo(2));
				Assert.That(doctors.First().Id, Is.Not.EqualTo(doctors.Last().Id));
			});
		}

		[Test]
		public void TestGetAllPatients()
		{
			// Act
			var patients = _userRepository!.GetAllPatients();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(patients, Is.Not.Null);
				Assert.That(patients.Count(), Is.EqualTo(2));
				Assert.That(patients.First().Id, Is.Not.EqualTo(patients.Last().Id));
			});
		}

		[Test]
		public void TestGetAllAdmins()
		{
			// Act
			var admins = _userRepository!.GetAllAdmin();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(admins, Is.Not.Null);
				Assert.That(admins.Count(), Is.EqualTo(2));
				Assert.That(admins.First().Id, Is.Not.EqualTo(admins.Last().Id));
			});
		}

		[Test]
		public void TestGetAllHospitalUsers()
		{
			// Act
			var hospitalUsers = _userRepository!.GetAllHospitalUsersWithAddress();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(hospitalUsers, Is.Not.Null);
				Assert.That(hospitalUsers.Count(), Is.EqualTo(4));
				Assert.That(hospitalUsers.First().Id, Is.Not.EqualTo(hospitalUsers.Last().Id));
			});
		}

		[Test]
		public void TestAddUser()
		{
			// Arrange
			var address2 = new Address()
			{
				Id = 2,
				StreetNumber = "456",
				StreetName = "Nice Street",
				Suburb = "Glenfield",
				State = "NSW",
				Postcode = "2167"
			};

			var newPatient = new Patient()
			{
				Id = 7,
				Firstname = "Santa",
				Lastname = "Claus",
				Address = address2
			};

			// Act
			_userRepository!.Add(newPatient);

			// Assert
			Assert.That(_userRepository.GetAllHospitalUsersWithAddress().Count(), Is.EqualTo(5));
		}

		[Test]
		public void TestAddRangeOfUsers()
		{
			// Arrange
			var address2 = new Address()
			{
				Id = 2,
				StreetNumber = "456",
				StreetName = "Nice Street",
				Suburb = "Glenfield",
				State = "NSW",
				Postcode = "2167"
			};

			var newPatient = new Patient()
			{
				Id = 7,
				Firstname = "Santa",
				Lastname = "Claus",
				Address = address2
			};

			var newPatient2 = new Patient()
			{
				Id = 8,
				Firstname = "Mrs",
				Lastname = "Claus",
				Address = address2
			};

			// Act
			_userRepository!.AddRange([newPatient, newPatient2]);

			// Assert
			Assert.That(_userRepository.GetAllHospitalUsersWithAddress().Count(), Is.EqualTo(6));
		}

		[Test]
		public void TestFindUsers()
		{
			// Act
			var neil = _userRepository!.Find(u => u.Id == 3).FirstOrDefault();
			var nullman = _userRepository!.Find(u => u.Id == -10);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(nullman.Count(), Is.Zero);
				Assert.That(neil, Is.Not.Null);
				Assert.That(neil!.Firstname, Is.EqualTo("neil"));
				Assert.That(neil.Lastname, Is.EqualTo("armstong"));
			});
		}

		[Test]
		public void TestGetAll()
		{
			// Act
			var allUsers = _userRepository!.GetAll();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(allUsers, Is.Not.Null);
				Assert.That(allUsers!.Count(), Is.EqualTo(6));
			});
		}
	}
}
