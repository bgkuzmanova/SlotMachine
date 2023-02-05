using Newtonsoft.Json;
using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using SlotMachine.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Data.Services
{
    public class ApplicationSettingsService : IApplicationSettingsService
    {
        public IScreenSettings GetScreenSettings(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            IScreenSettings screenSettings = new ScreenSettings();

            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                screenSettings = JsonConvert.DeserializeObject<ScreenSettings>(json);
            }
            return screenSettings;
        }
    }
}
