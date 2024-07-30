using HospitalManagementSystem;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HospitalManagementSystemTests
{
	public class AppointmentRepositoryTests
	{
		Mock<HospitalManagementSystemContext>? _mockContext;
		DbSet<Appointment>? _userSetObject;
		AppointmentRepository? _appointmentRepository;

		readonly Doctor[] _doctors = TestData.GetSampleDoctors([0]);
		readonly Patient[] _patients = TestData.GetSamplePatients([0]);

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

			List<Appointment> appointments = [];

			for (int i = 0; i < 5; i++)
			{
				appointments.Add(new Appointment()
				{
					Id = i + 1,
					PatientId = _patients[i].Id,
					DoctorId = _doctors[i].Id,
					Description = "Covid!!"
				});
			}

			_userSetObject = MockDbHelpers.GetQueryableMockDbSet(appointments);
			_mockContext = new Mock<HospitalManagementSystemContext>();
			_mockContext.Setup(h => h.Appointments).Returns(_userSetObject);
			_mockContext.Setup(h => h.Set<Appointment>()).Returns(_userSetObject);
			_appointmentRepository = new AppointmentRepository(_mockContext.Object);
		}


		[Test]
		public void TestAddUser()
		{
			// Arrange
			var appointment = new Appointment()
			{
				Id = 6,
				PatientId = 6,
				DoctorId = 6,
				Description = "Heart attack!"
			};

			// Act
			_appointmentRepository!.Add(appointment);

			// Assert
			Assert.That(_appointmentRepository.GetAll().Count(), Is.EqualTo(6));
			Assert.That(_appointmentRepository.GetAll().GroupBy(a => a.Id).Count(), Is.EqualTo(6));
		}

		[Test]
		public void TestAddRangeOfUsers()
		{
			// Arrange
			var appointments = new List<Appointment>();

			for (int i = 6; i < 11; i++)
			{
				appointments.Add(new Appointment()
				{
					Id = i,
					PatientId = i,
					DoctorId = i,
					Description = "Influenza :("
				});
			}

			// Act
			_appointmentRepository!.AddRange(appointments);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(_appointmentRepository.GetAll().Count(), Is.EqualTo(10));
				Assert.That(_appointmentRepository.GetAll().GroupBy(a => a.Id).Count(), Is.EqualTo(10));
			});
		}

		[Test]
		public void TestFindUsers()
		{
			// Act
			var thirdAppointment = _appointmentRepository!.Find(u => u.Id == 3).FirstOrDefault();
			var nullAppointment = _appointmentRepository!.Find(u => u.Id == -10);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(nullAppointment.Count(), Is.Zero);
				Assert.That(thirdAppointment, Is.Not.Null);
				Assert.That(thirdAppointment!.Id, Is.EqualTo(3));
				Assert.That(thirdAppointment.PatientId, Is.EqualTo(103));
				Assert.That(thirdAppointment.DoctorId, Is.EqualTo(3));
			});
		}

		[Test]
		public void TestGetAll()
		{
			// Act
			var allUsers = _appointmentRepository!.GetAll();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(allUsers, Is.Not.Null);
				Assert.That(allUsers!.Count(), Is.EqualTo(5));
				Assert.That(allUsers!.GroupBy(u => u.Id).Count(), Is.EqualTo(5));
			});
		}
	}
}
