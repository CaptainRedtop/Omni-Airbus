using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    public class SortingSystem : Consumer
    {
        public int BaggageID;
        public DateTime InboundTime;
        public DateTime OutboundTime;
        private Queue<Luggage> InboundConveyorBelt;
        private Queue<Luggage> OutboundConveyorBelt;
        internal Assembly assembly = Assembly.GetExecutingAssembly();

        public SortingSystem(ConveyerBelt inboundbelt) : base(inboundbelt)
        {
            InboundConveyorBelt = new Queue<Luggage>();
            OutboundConveyorBelt = new Queue<Luggage>();
        }

        public void PullFromBelts(object obj)
        {
            for (int i = 0; true;)
            {
                Type[] types = assembly.GetTypes();
                List<CheckIn> checkIns = new List<CheckIn>();
                foreach (Type type in types)
                {
                    if (typeof(CheckIn).IsAssignableFrom(type))
                    {
                        CheckIn checkIn = (CheckIn)Activator.CreateInstance(type);
                        checkIns.Add(checkIn);
                    }
                }

                if (CurrentLuggage == null)
                {
                    if (checkIns[i % checkIns.Count].OutboundBelt.Count > 0)
                    {
                        CurrentLuggage = checkIns[i % checkIns.Count].OutboundBelt.Dequeue();
                        break;
                    }
                }
            }
        }

        public void SendToGate(object obj)
        {
            while (true)
            {
                Type[] types = assembly.GetTypes();
                List<Consumer> consumers = new List<Consumer>();

                foreach (Type type in types)
                {
                    if (typeof(Consumer).IsAssignableFrom(type))
                    {
                        Consumer consumer = (Consumer)Activator.CreateInstance(type);
                        consumers.Add(consumer);
                    }
                }

                foreach (Consumer consumer in consumers)
                {
                    if (consumer.CurrentFlightID == CurrentLuggage.flightID)
                    {
                        consumer.InboundBelt.Enqueue(CurrentLuggage);
                        CurrentLuggage = null;
                        break;
                    }
                }
            }
        }
    }
}
