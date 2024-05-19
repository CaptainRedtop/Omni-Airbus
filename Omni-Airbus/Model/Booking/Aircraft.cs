using Omni_Airbus.Utils;

namespace Omni_Airbus.Model.Booking
{
    /// <summary>
    /// This gets the aircraft information with an ID, model, and total seats.
    /// </summary>
    public class Aircraft
    {
        private int _aircraftId;
        private string _model;
        private int _totalSeats;

        public Aircraft(int aircraftId)
        {
            AircraftId = aircraftId;
            foreach (object[] aircraftObject in MySQL.DBStatement($"CALL GetAircraftDetails({aircraftId})"))
            {
                Model = (string)aircraftObject[0];
                TotalSeats = (int)aircraftObject[1];
                break;
            }
        }

        public int AircraftId
        {
            get { return _aircraftId; }
            set { _aircraftId = value; }
        }

        public string Model
        {
            get { return _model; }
            set { _model = value; }
        }

        public int TotalSeats
        {
            get { return _totalSeats; }
            set { _totalSeats = value; }
        }
    }
}
