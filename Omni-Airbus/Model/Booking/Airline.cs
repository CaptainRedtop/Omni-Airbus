namespace Omni_Airbus.Model.Booking
{
	/// <summary>
	/// This represents an airline with an ID and a name.
	/// </summary>
	public class Airline
	{
		private int _airlineId; 
		private string _name; 

		public int AirlineId
		{
			get { return _airlineId; }
			set { _airlineId = value; }
		}

		public string Name
		{
			get { return _name; }
			set { _name = value; }
		}
	}
}
