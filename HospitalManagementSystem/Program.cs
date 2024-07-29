using Microsoft.Extensions.DependencyInjection;

namespace HospitalManagementSystem
{
	public class Program
	{
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