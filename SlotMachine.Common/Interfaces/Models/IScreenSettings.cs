namespace SlotMachine.Common.Interfaces.Models
{
    public interface IScreenSettings
    {
        int Rows { get; set; }
        int Columns { get; set; }
        int InputTrials { get; set; }
    }
}
