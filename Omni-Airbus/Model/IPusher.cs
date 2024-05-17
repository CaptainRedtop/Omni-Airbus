using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Model
{
    /// <summary>
    /// Defines the functionality for an object that can push luggage to a conveyorbelt
    /// </summary>
    internal interface IPusher
    {
        /// <summary>
        /// A blueprint for the Push method
        /// </summary>
        /// <param name="obj"></param>
        void Push(object obj);
    }
}
