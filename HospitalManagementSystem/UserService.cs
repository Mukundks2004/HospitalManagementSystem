namespace HospitalManagementSystem
{
	public class UserService
	{
		readonly UserRepository _userRepository;

		public UserService(UserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		/// <summary>
		/// Returns all users including addresses for hospital users
		/// </summary>
		/// <returns></returns>
		public IEnumerable<User> GetAllUsersWithAddresses()
		{
			var hospitalUsers = _userRepository.GetAllHospitalUsersWithAddress().ToList<User>();
			var admin = _userRepository.GetAllAdmin().ToList<User>();
			return hospitalUsers.Concat(admin);
		}
	}
}
