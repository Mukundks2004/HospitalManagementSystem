namespace HospitalManagementSystem
{
	public class DoctorService
	{
		readonly UserRepository _userRepository;

		public DoctorService(UserRepository userRepository)
		{
			_userRepository = userRepository;
		}
	}
}
