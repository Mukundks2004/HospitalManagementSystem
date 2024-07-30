namespace HospitalManagementSystem
{
	public class AdminService
	{
		readonly UserRepository _userRepository;

		public AdminService(UserRepository userRepository)
		{
			_userRepository = userRepository;
		}
	}
}
