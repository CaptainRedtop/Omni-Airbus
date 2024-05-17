using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model.Booking
{
	/// <summary>
	/// This represents an airline with an ID and a name.
	/// </summary>
	internal class Airline
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
