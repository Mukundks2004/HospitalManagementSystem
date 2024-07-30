namespace HospitalManagementSystem
{
	public class AdminService
	{
		readonly UserRepository _userRepository;

		string _feedback = string.Empty;

		public AdminService(UserRepository userRepository)
		{
			_userRepository = userRepository;
		}
	}
}
