namespace HospitalManagementSystem
{
	public class IdGenerator
	{
		int _currentId;

		public int CurrentId
		{
			get => ++_currentId;
		}

		public IdGenerator(int startingId)
		{
			_currentId = startingId;
		}
	}
}
