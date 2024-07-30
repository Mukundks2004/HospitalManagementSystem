using HospitalManagementSystem;

namespace HospitalManagementSystemTests
{
	public class UtilitiesTests
	{
		const string OneBoxText = "╔─────────────────────────────────────┐\r\n│  DOTNET Hospital Management System  │\r\n├─────────────────────────────────────┤\r\n│                  1                  │\r\n└─────────────────────────────────────┘\r\n";
		const string HelloBoxText = "╔─────────────────────────────────────┐\r\n│  DOTNET Hospital Management System  │\r\n├─────────────────────────────────────┤\r\n│                Hello                │\r\n└─────────────────────────────────────┘\r\n";
		const string LongMessageBoxText = "╔─────────────────────────────────────┐\r\n│  DOTNET Hospital Management System  │\r\n├─────────────────────────────────────┤\r\n│      This is a longer message       │\r\n└─────────────────────────────────────┘\r\n";

		[SetUp]
		public void Setup()
		{

		}

		[TearDown]
		public void Teardown()
		{

		}

		[TestCase("1", OneBoxText)]
		[TestCase("Hello", HelloBoxText)]
		[TestCase("This is a longer message", LongMessageBoxText)]
		public void PrintMessageInBoxFormatsCorrectly(string message, string expectedOutput)
		{
			// Arrange
			var sw = new StringWriter();
			Console.SetOut(sw);

			// Act
			Utilities.PrintMessageInBox(message);

			// Assert
			Assert.That(sw.ToString(), Is.EqualTo(expectedOutput));
		}

		[TestCase("Knock Knock", "Who's there?")]
		[TestCase("Prompt", "Response")]
		[TestCase("", "")]
		public void ReadLineReadsAndPrintsPrompt(string prompt, string response)
		{
			// Arrange
			var sw = new StringWriter();
			Console.SetOut(sw);

			var sr = new StringReader(response);
			Console.SetIn(sr);

			// Act
			Utilities.ReadLine(prompt);

			// Assert
			Assert.That(sw.ToString(), Is.EqualTo(prompt + response));
		}
	}
}