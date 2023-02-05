using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Common.Interfaces.Views
{
    public interface IViewManager
    {
        void ViewOutput(string s);
        void ViewOutput(char ch);
        string ViewInput();
    }
}
