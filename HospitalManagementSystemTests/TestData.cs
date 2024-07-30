namespace HospitalManagementSystem
{
	public static class TestData
	{
		public static Doctor[] GetSampleDoctors(int?[] addressIds)
		{
			return
			[
				new Doctor
				{
					Id = 1,
					Password = "apple",
					Firstname = "Mukund",
					Lastname = "Srinivasan",
					AddressId = addressIds[0],
					Email = "mks@gmail.com",
					Phone = "0400000000"
				},
				new Doctor
				{
					Id = 2,
					Password = "banana",
					Firstname = "Ananya",
					Lastname = "Srinivasan",
					AddressId = addressIds[0],
					Email = "ananya@gmail.com",
					Phone = "0400000001"
				},
				new Doctor
				{
					Id = 3,
					Password = "cherry",
					Firstname = "Davey",
					Lastname = "Dyer",
					AddressId = addressIds[0],
					Email = "davey@gmail.com",
					Phone = "0400000002"
				},
				new Doctor
				{
					Id = 4,
					Password = "onion",
					Firstname = "Sam",
					Lastname = "Rothwell",
					AddressId = addressIds[0],
					Email = "sammy@gmail.com",
					Phone = "0400000003"
				},
				new Doctor
				{
					Id = 5,
					Password = "blueberry",
					Firstname = "Avinash",
					Lastname = "Singh",
					AddressId = addressIds[0],
					Email = "avi@gmail.com",
					Phone = "0400000004"
				},
				new Doctor
				{
					Id = 6,
					Password = "tomato",
					Firstname = "Sophia",
					Lastname = "Nguyen",
					AddressId = addressIds[0],
					Email = "soph@gmail.com",
					Phone = "0400000005"
				},
				new Doctor
				{
					Id = 7,
					Password = "turtle",
					Firstname = "Ethan",
					Lastname = "Mangan",
					AddressId = addressIds[0],
					Email = "e-man@gmail.com",
					Phone = "0400000006"
				},
				new Doctor
				{
					Id = 8,
					Password = "table",
					Firstname = "Guhan",
					Lastname = "Sundar",
					AddressId = addressIds[0],
					Email = "da_fish@gmail.com",
					Phone = "0400000007"
				},
				new Doctor
				{
					Id = 9,
					Password = "scarlett",
					Firstname = "Sally",
					Lastname = "Luu",
					AddressId = addressIds[0],
					Email = "seely@gmail.com",
					Phone = "0400000008"
				},
				new Doctor
				{
					Id = 10,
					Password = "karate",
					Firstname = "David",
					Lastname = "Sorrell",
					AddressId = addressIds[0],
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
					Id = 101,
					Password = "coconut",
					Firstname = "sample_F",
					Lastname = "sample_L",
					AddressId = addressIds[0],
					//Email = "mukundsrinivasan0@gmail.com",
					Email = "muk@gmail.com",
					Phone = "0400000010"
				},
				new Patient
				{
					Id = 102,
					Password = "rocks",
					Firstname = "Aiden",
					Lastname = "Gardner",
					AddressId = addressIds[0],
					Email = "climber@gmail.com",
					Phone = "0400000011"
				},
				new Patient
				{
					Id = 103,
					Password = "bacon",
					Firstname = "Peppa",
					Lastname = "Pig",
					AddressId = addressIds[0],
					Email = "superman@gmail.com",
					Phone = "0400000012"
				},
				new Patient
				{
					Id = 104,
					Password = "donut",
					Firstname = "Josh",
					Lastname = "Roy",
					AddressId = addressIds[0],
					Email = "fastguy@gmail.com",
					Phone = "0400000013"
				},
				new Patient
				{
					Id = 105,
					Password = "lizard",
					Firstname = "Donald",
					Lastname = "Trump",
					AddressId = addressIds[0],
					Email = "orange@gmail.com",
					Phone = "0400000014"
				},
				new Patient
				{
					Id = 106,
					Password = "iphone",
					Firstname = "Jane",
					Lastname = "Citizen",
					AddressId = addressIds[0],
					Email = "jane@gmail.com",
					Phone = "0400000015"
				},
				new Patient
				{
					Id = 107,
					Password = "passionfruit",
					Firstname = "Sam",
					Lastname = "Sparks",
					AddressId = addressIds[0],
					Email = "reporter@gmail.com",
					Phone = "0400000016"
				},
				new Patient
				{
					Id = 108,
					Password = "tea",
					Firstname = "John",
					Lastname = "Wick",
					AddressId = addressIds[0],
					Email = "dog@gmail.com",
					Phone = "0400000017"
				},
				new Patient
				{
					Id = 109,
					Password = "chair",
					Firstname = "Liam",
					Lastname = "Neeson",
					AddressId = addressIds[0],
					Email = "taken@gmail.com",
					Phone = "0400000018"
				},
				new Patient
				{
					Id = 110,
					Password = "suitcase",
					Firstname = "Tom",
					Lastname = "Hanks",
					AddressId = addressIds[0],
					Email = "airport@gmail.com",
					Phone = "0400000019"
				},
			];
		}
	}
}
