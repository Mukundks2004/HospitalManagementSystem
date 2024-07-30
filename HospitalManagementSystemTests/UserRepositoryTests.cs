using HospitalManagementSystem;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HospitalManagementSystemTests
{
	public class UserRepositoryTests
	{
		Mock<HospitalManagementSystemContext>? _mockContext;
		Mock<DbSet<User>>? _mockUserSet;
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

			_mockUserSet = MockDbHelpers.GetQueryableMockDbSet(new List<User> { doctor });
			_mockContext = new Mock<HospitalManagementSystemContext>();
			_mockContext.Setup(h => h.Users).Returns(_mockUserSet.Object);
			_userRepository = new UserRepository(_mockContext.Object);
		}

		[Test]
		public void TestAddUser()
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
	}

}
