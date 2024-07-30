using HospitalManagementSystem;

namespace HospitalManagementSystemTests
{
	public class IdGeneratorTests
	{
		[TestCase(0)]
		[TestCase(10)]
		[TestCase(100)]
		[TestCase(5000)]
		public void IdStartsAtSetValueAndChanges(int idStartingValue)
		{
			// Arrange
			var idGenerator = new IdGenerator(idStartingValue);

			// Act
			var currentId = idGenerator.CurrentId;
			var nextCurrentId = idGenerator.CurrentId;

			// Assert
			Assert.That(currentId, Is.EqualTo(idStartingValue + 1));
			Assert.That(nextCurrentId, Is.EqualTo(idStartingValue + 2));
		}

		[TestCase(5)]
		[TestCase(3000)]
		[TestCase(400000)]
		public void CallingGenerateIncrements(int idStartingValue)
		{
			// Arrange
			var idGenerator = new IdGenerator(idStartingValue);
			int currentId;

			// Act / Assert
			for (int i = 1; i <= 100; i++)
			{
				currentId = idGenerator.CurrentId;
				Assert.That(currentId, Is.EqualTo(idStartingValue + i));
			}
		}
	}
}