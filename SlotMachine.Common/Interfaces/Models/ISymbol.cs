namespace SlotMachine.Common.Interfaces.Models
{
    public interface ISymbol
    {
        char SymbolChar { get; set; }
        string? FullName { get; set; }
        decimal Coefficient { get; set; }
        int Probability { get; set; }
    }
}