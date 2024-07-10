using System.Reflection.Metadata;

namespace HospitalManagementSystem
{
	public class Program
	{
		public static void Main(string[] args)
		{
			using (var db = new HospitalManagementSystemContext())
			{
				// Create and save a new Blog
				Console.Write("Enter a name for a new Blog: ");
				var name = Console.ReadLine();

				//var patient = new Patient(100, "2", "3", "4", new Address(5, "6", "7", "8", "*8", "gg"), "9", "0");
				var p = new Patient() { Firstname = "uwu boss" };
				db.Patients.Add(p);
				db.SaveChanges();

				// Display all Blogs from the database
				var query = from b in db.Patients
							orderby b.Firstname
							select b;

				Console.WriteLine("All patients in the database:");
				foreach (var item in query)
				{
					Console.WriteLine(item.Firstname);
				}

				Console.WriteLine("Press any key to exit...");
				Console.ReadKey();
			}
			//var app = new Application();
			//app.Run();
		}
	}
}