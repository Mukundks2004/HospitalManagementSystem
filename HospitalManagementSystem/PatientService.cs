namespace HospitalManagementSystem
{
	public class PatientService
	{
		readonly UserRepository _userRepository;

		public PatientService(UserRepository userRepository)
		{
			_userRepository = userRepository;
		}
	}
}
