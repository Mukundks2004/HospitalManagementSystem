namespace HospitalManagementSystem
{
	public static class Constants
	{
		//public static char TopLeft = '┌';
		public static readonly char TopLeft = '╔';
		public static readonly char TopRight = '┐';
		public static readonly char MiddleLeft = '├';
		public static readonly char MiddleRight = '┤';
		public static readonly char BottomLeft = '└';
		//public static readonly char BottomMiddle = '┴';
		public static readonly char BottomRight = '┘';
		public static readonly char HorizontalLine = '─';
		public static readonly char VerticalLine = '│';
		public static readonly char Center = '┼';

		public static readonly string BoxHeader = "DOTNET Hospital Management System";
		public static readonly int BoxPadding = 2;

		public static readonly Dictionary<string, string> TempDetails = new() { { "admin", "123" } };
	}
}