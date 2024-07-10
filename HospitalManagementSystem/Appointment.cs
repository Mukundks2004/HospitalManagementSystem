using System.Numerics;

namespace HospitalManagementSystem
{
	public class Appointment(string id, Doctor doctor, Patient patient, string description)
	{
		public string Id { get; set; } = id;

		public Doctor Doctor { get; set; } = doctor;

		public Patient Patient { get; set; } = patient;

		public string Description { get; set; } = description;

		public override string ToString()
		{
			return "ko";
			//return $"{Id, -6}{Constants.VerticalLine} {Patient.GetFullName(), -19}{Constants.VerticalLine} {Doctor.GetFullName(), -19}{Constants.VerticalLine} {Description}";
		} 

		public static void PrintDetailsHeader()
		{
			Console.WriteLine($"ID{new string(' ', 4)}{Constants.VerticalLine} Doctor{new string(' ', 13)}{Constants.VerticalLine} Patient{new string(' ', 12)}{Constants.VerticalLine} Description");
			Console.WriteLine($"{new string(Constants.HorizontalLine, 6)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 20)}{Constants.Center}{new string(Constants.HorizontalLine, 35)}");
		}
	}
}