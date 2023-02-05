using SlotMachine.Common.Interfaces.Models;

namespace SlotMachine.Common.Interfaces.Services
{
    public interface ISymbolsDataService
    {
        IEnumerable<ISymbol> GetSymbols(string fileName);
    }
}
