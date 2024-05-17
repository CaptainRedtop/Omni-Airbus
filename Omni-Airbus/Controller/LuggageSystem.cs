using Omni_Airbus.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Omni_Airbus.Controller
{
    internal class LuggageSystem
    {
        public bool CheckIn;

        private static ConveyerBelt CheckInBelt1 = new ConveyerBelt();
        private static ConveyerBelt CheckInBelt2 = new ConveyerBelt();
        private static ConveyerBelt TerminalBelt1 = new ConveyerBelt();
        private static ConveyerBelt TerminalBelt2 = new ConveyerBelt();

        private static CheckIn CheckIn1 = new CheckIn(CheckInBelt1);
        private static CheckIn CheckIn2 = new CheckIn(CheckInBelt2);

        private static Terminal Terminal1 = new Terminal(TerminalBelt1);
        private static Terminal Terminal2 = new Terminal(TerminalBelt2);

        public void RunLuggageSystem()
        {
            ThreadPool.QueueUserWorkItem(CheckIn1.Checked)
        }
    }
}
