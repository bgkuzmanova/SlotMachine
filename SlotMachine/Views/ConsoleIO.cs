using SlotMachine.Common.Interfaces.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Views
{
    public class ConsoleIO : IViewManager
    {
        public void ViewOutput(string s)
        {
            Console.WriteLine(s);
        }
        public void ViewOutput(char ch)
        {
            Console.Write(ch);
        }
        public string ViewInput()
        {
            return Console.ReadLine();
        }

    }
}
