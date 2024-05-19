using Omni_Airbus.Controller;
using Omni_Airbus.Model.Booking;
using Omni_Airbus.Utils;
using Omni_Airbus.Utils.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    public class Terminal : Consumer
    {
        public ConveyerBelt InboundBelt;
        public Luggage? CurrentLuggage;
        private Logger log;
        List<CheckIn> CheckIns = new List<CheckIn>();
        public bool Gate;
        private readonly object obj = new object();
        int LuggageCount = 0;
        private Aircraft? CurrentAircraft;
        public bool open = false;
        public int ID;

        public Terminal(ConveyerBelt inboundbelt, CheckIn CheckIn1, CheckIn CheckIn2, int id) : base(inboundbelt)
        {
            CheckIns.Add(CheckIn1);
            CheckIns.Add(CheckIn2);
            InboundBelt = inboundbelt;
            log = new Logger(0);
            ID = id;
        }

        #region Pull
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        public override void Pull(object obj)
        {
            while (true)
            {
                //if we have no flight, get a flight
                SecureFlight();
                TryRenewAircraft();

                if (open)
                {
                    if (LuggageCount < CurrentAircraft.TotalSeats)
                    {
                        CurrentLuggage = InboundBelt.Dequeue();
                        if (CurrentLuggage != null)
                        {
                            log.Information($"Loading Luggage: {CurrentLuggage.baggageID}");
                        }
                    }
                }
                Thread.Sleep(0.01f.ToMilliseconds());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void TryRenewAircraft()
        {
            //if current aircrafat is null or full
            if (CurrentAircraft == null)
            {
                GetNewAircraft();
                foreach (CheckIn checkIn in CheckIns)
                {
                    if (checkIn.CurrentFlight == null)
                    {
                        checkIn.CurrentFlight = CurrentFlight;
                    }
                }
                SetCheckIn(true);
            }
            else if (LuggageCount >= CurrentAircraft.TotalSeats)
            {
                SetCheckIn(false);
                UpdateCurrentFlight();
                GetNewAircraft();
                SetCheckIn(true);
                Report.Aircrafts.Add(CurrentFlight.AircraftId);
                Report.Desinations.Add(CurrentFlight.DestinationId);
                Report.Airlines.Add(CurrentFlight.AirlineId);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void UpdateCurrentFlight()
        {
            if (CurrentFlight == null)
            {
                SetCurrentFlight();
                foreach (CheckIn checkIn in CheckIns)
                {
                    if (checkIn.CurrentFlight == null)
                    {
                        checkIn.CurrentFlight = CurrentFlight;
                        break;
                    }
                    else if (checkIn.CurrentFlight == CurrentFlight)
                    {
                        checkIn.CurrentFlight = CurrentFlight;
                    }
                }
            }
            else
            {
                foreach (CheckIn checkIn in CheckIns)
                {
                    if (checkIn.CurrentFlight == CurrentFlight)
                    {
                        SetCurrentFlight();
                        checkIn.CurrentFlight = CurrentFlight;
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetCurrentFlight()
        {
            lock (LuggageSystem.Flights)
            {
                CurrentFlight = LuggageSystem.Flights.Dequeue();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void GetNewAircraft()
        {
            if (CurrentAircraft != null)
            {
                log.Information($"Aircraft: {CurrentAircraft.AircraftId} is full");
            }
            CurrentAircraft = new Aircraft(CurrentFlight.AircraftId);
            log.Information($"New Aircraft: {CurrentFlight.AircraftId}");
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetCheckIn(bool value)
        {
            open = value;
            foreach (CheckIn checkIn in CheckIns)
            {
                bool FlightCheckIn = checkIn.CurrentFlight != null;
                bool FlightTerminal = CurrentFlight != null;
                bool FlightMatch = checkIn.CurrentFlight.FlightId == CurrentFlight.FlightId;
                if (FlightCheckIn && FlightTerminal && FlightMatch)
                {
                    checkIn.open = value;
                    log.Information($"Set CheckIn: {checkIn.ID} To: {value}");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SecureFlight()
        {
            if (CurrentFlight == null)
            {
                lock (LuggageSystem.Flights)
                {
                    CurrentFlight = LuggageSystem.Flights.Dequeue();
                }
            }
        }
        #endregion
    }
}
