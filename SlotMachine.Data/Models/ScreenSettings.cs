using SlotMachine.Common.Interfaces.Models;

namespace SlotMachine.Data.Models
{
    public class ScreenSettings : IScreenSettings
    {
        public int Rows { get; set; }
        public int Columns { get; set; }
        public int InputTrials { get; set; }
    }
}
