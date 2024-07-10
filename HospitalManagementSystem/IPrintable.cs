namespace HospitalManagementSystem
{
	//This interface is currently not used but we can make it used
	//All you have to do is make the repository class generic for example GetTypeById<T>
	//If the above is possible, you can pass the type directly, and call this method on the type
	//Might need reflection
	public interface IPrintable
	{
		public void PrintDetailsHeader();
	}
}