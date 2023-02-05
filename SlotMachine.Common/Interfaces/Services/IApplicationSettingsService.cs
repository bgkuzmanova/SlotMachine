using SlotMachine.Common.Interfaces.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Common.Interfaces.Services
{
    public interface IApplicationSettingsService
    {
        IScreenSettings GetScreenSettings(string fileName);
    }
}
