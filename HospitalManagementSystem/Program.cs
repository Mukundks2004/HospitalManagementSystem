using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem
{
	public class Program
	{
		/// <summary>
		/// Entry point for program- sets up dependency injection for all repositories and services and calls application
		/// </summary>
		/// <param name="args"></param>
		/// <exception cref="ArgumentNullException"></exception>
		public static void Main(string[] args)
		{
			ServiceProvider serviceProvider = new ServiceCollection()
				.AddDbContext<HospitalManagementSystemContext>()
				.AddScoped<UserRepository>()
				.AddScoped<AddressRepository>()
				.AddScoped<AppointmentRepository>()
				.AddScoped<HospitalService>()
				.BuildServiceProvider();

			HospitalService? service = serviceProvider.GetService<HospitalService>();
			_ = service ?? throw new ArgumentNullException(nameof(service));

			var app = new Application(service);
			app.Run();
		}
	}
}