using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    public class ConveyerBelt
    {
        public const int MAX_SIZE = 100;
        private Queue<Luggage> LuggageItems;
        private readonly object lockObject = new object();
        public readonly int ID;

        /// <summary>
        /// 
        /// </summary>
        public ConveyerBelt(int iD)
        {
            LuggageItems = new Queue<Luggage>();
            ID = iD;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        public void Enqueue(Luggage item)
        {
            lock (lockObject)
            {
                if (LuggageItems.Count < MAX_SIZE)
                {
                    LuggageItems.Enqueue(item);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Luggage Dequeue()
        {
            lock (lockObject)
            {
                return LuggageItems.Count > 0 ? LuggageItems.Dequeue() : null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public int Count
        {
            get
            {
                lock (lockObject)
                {
                    return LuggageItems.Count;
                }
            }
        }
    }
}
