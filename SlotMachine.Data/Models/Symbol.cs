using SlotMachine.Common.Interfaces.Models;

namespace SlotMachine.Data.Models
{
    public class Symbol : ISymbol
    {
        public char SymbolChar { get; set; }
        public string? FullName { get; set; }
        public decimal Coefficient { get; set; }
        public int Probability { get; set; }
    }
}
