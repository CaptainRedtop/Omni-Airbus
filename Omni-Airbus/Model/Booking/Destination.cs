namespace Omni_Airbus.Model.Booking
{
	/// <summary>
	/// Destination with an ID, city, country, and airport code.
	/// </summary>
	public class Destination
	{
		private int _destinationId;
		private string _city;
		private string _country;
		private string _airportCode;

		public int DestinationId
		{
			get { return _destinationId; }
			set { _destinationId = value; }
		}

		public string City
		{
			get { return _city; }
			set { _city = value; }
		}

		public string Country
		{
			get { return _country; }
			set { _country = value; }
		}

		public string AirportCode
		{
			get { return _airportCode; }
			set { _airportCode = value; }
		}
	}
}
