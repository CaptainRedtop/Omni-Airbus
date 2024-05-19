
using Omni_Airbus.Utils;

namespace Omni_Airbus.Model.Booking
{
    /// <summary>
    /// "Flight" with ID, flight number, departure time, arrival time, airline ID, aircraft ID, and destination ID.
    /// </summary>
    public class Flight
    {
        private int _flightId;
        private string _flightNumber;
        private DateTime _departureTime;
        private DateTime _arrivalTime;
        private int _airlineId;
        private int _aircraftId;
        private int _destinationId;

        public Flight(int flightId, string flightNumber, DateTime departureTime, DateTime arrivalTime, int airlineId, int aircraftId, int destinationId)
        {
            FlightId = flightId;
            FlightNumber = flightNumber;
            DepartureTime = departureTime;
            ArrivalTime = arrivalTime;
            AirlineId = airlineId;
            AircraftId = aircraftId;
            DestinationId = destinationId;
        }

        public int FlightId
        {
            get { return _flightId; }
            set { _flightId = value; }
        }

        public string FlightNumber
        {
            get { return _flightNumber; }
            set { _flightNumber = value; }
        }

        public DateTime DepartureTime
        {
            get { return _departureTime; }
            set { _departureTime = value; }
        }

        public DateTime ArrivalTime
        {
            get { return _arrivalTime; }
            set { _arrivalTime = value; }
        }

        public int AirlineId
        {
            get { return _airlineId; }
            set { _airlineId = value; }
        }

        public int AircraftId
        {
            get { return _aircraftId; }
            set { _aircraftId = value; }
        }

        public int DestinationId
        {
            get { return _destinationId; }
            set { _destinationId = value; }
        }

        /// <summary>
        /// Get all flgihts form the database.
        /// </summary>
        /// <returns></returns>
        public static Queue<Flight> GetFlights()
        {
            List<object[]> sqlData = MySQL.DBStatement("CALL GetFlights();");
            Queue<Flight> flights = new Queue<Flight>();
            foreach (object[] row in sqlData)
            {
                flights.Enqueue(new Flight(
                    (int)row[0],
                    (string)row[1],
                    (DateTime)row[2],
                    (DateTime)row[3],
                    (int)row[4],
                    (int)row[5],
                    (int)row[6])
                    );
            }
            return flights;
        }
    }
}