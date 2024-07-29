namespace HospitalManagementSystem
{
	public class AppointmentRepository : Repository<Appointment>
	{
		public AppointmentRepository(HospitalManagementSystemContext context) : base(context)
		{
		}
	}
}