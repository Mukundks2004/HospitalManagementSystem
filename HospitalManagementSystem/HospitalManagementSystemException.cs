namespace HospitalManagementSystem
{
	public class HospitalManagementSystemException : Exception
	{
		public HospitalManagementSystemException()
		{
		}

		public HospitalManagementSystemException(string message) : base(message)
		{
		}

		public HospitalManagementSystemException(string message, Exception inner) : base(message, inner)
		{
		}
	}
}