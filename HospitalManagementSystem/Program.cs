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
				.AddScoped<Application>()
				.BuildServiceProvider();

			Application? app = serviceProvider.GetService<Application>();
			_ = app ?? throw new ArgumentNullException(nameof(app));

			app.Run();
		}
	}
}