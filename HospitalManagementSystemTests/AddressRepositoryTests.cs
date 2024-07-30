using HospitalManagementSystem;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace HospitalManagementSystemTests
{
	public class AddressRepositoryTests
	{
		Mock<HospitalManagementSystemContext>? _mockContext;
		DbSet<Address>? _userSetObject;
		AddressRepository? _addressRepository;

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

			var northPole = new Address()
			{
				Id = 2,
				StreetNumber = "1",
				StreetName = "Santa Way",
				Suburb = "Glacier",
				State = "SSS",
				Postcode = "9999"
			};

			var uts = new Address()
			{
				Id = 3,
				StreetNumber = "1",
				StreetName = "Chippendale Road",
				Suburb = "Ultimo",
				State = "NSW",
				Postcode = "2009"
			};

			_userSetObject = MockDbHelpers.GetQueryableMockDbSet(new List<Address> { address, northPole, uts });
			_mockContext = new Mock<HospitalManagementSystemContext>();
			_mockContext.Setup(h => h.Addresses).Returns(_userSetObject);
			_mockContext.Setup(h => h.Set<Address>()).Returns(_userSetObject);
			_addressRepository = new AddressRepository(_mockContext.Object);
		}


		[Test]
		public void TestAddUser()
		{
			// Arrange
			var address2 = new Address()
			{
				Id = 4,
				StreetNumber = "456",
				StreetName = "Nice Street",
				Suburb = "Glenfield",
				State = "NSW",
				Postcode = "2167"
			};

			// Act
			_addressRepository!.Add(address2);

			// Assert
			Assert.That(_addressRepository.GetAll().Count(), Is.EqualTo(4));
			Assert.That(_addressRepository.GetAll().GroupBy(a => a.Id).Count(), Is.EqualTo(4));
		}

		[Test]
		public void TestAddRangeOfUsers()
		{
			// Arrange
			var address1 = new Address()
			{
				Id = 4,
				StreetNumber = "456",
				StreetName = "Nice Street",
				Suburb = "Glenfield",
				State = "NSW",
				Postcode = "2167"
			};

			var address2 = new Address()
			{
				Id = 5,
				StreetNumber = "11",
				StreetName = "Montgomery Road",
				Suburb = "Bonyrigg",
				State = "NSW",
				Postcode = "2399"
			};

			// Act
			_addressRepository!.AddRange([address1, address2]);

			// Assert
			Assert.That(_addressRepository.GetAll().Count(), Is.EqualTo(5));
			Assert.That(_addressRepository.GetAll().GroupBy(a => a.Id).Count(), Is.EqualTo(5));
		}

		[Test]
		public void TestFindUsers()
		{
			// Act
			var neil = _addressRepository!.Find(u => u.Id == 3).FirstOrDefault();
			var nullman = _addressRepository!.Find(u => u.Id == -10);

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(nullman.Count(), Is.Zero);
				Assert.That(neil, Is.Not.Null);
				Assert.That(neil!.State, Is.EqualTo("NSW"));
				Assert.That(neil.StreetNumber, Is.EqualTo("1"));
				Assert.That(neil.Suburb, Is.EqualTo("Ultimo"));
				Assert.That(neil.StreetName, Is.EqualTo("Chippendale Road"));
			});
		}

		[Test]
		public void TestGetAll()
		{
			// Act
			var allUsers = _addressRepository!.GetAll();

			// Assert
			Assert.Multiple(() =>
			{
				Assert.That(allUsers, Is.Not.Null);
				Assert.That(allUsers!.Count(), Is.EqualTo(3));
				Assert.That(allUsers!.GroupBy(u => u.Id).Count(), Is.EqualTo(3));
			});
		}
	}
}
