using SlotMachine.Common.Interfaces.Models;

namespace SlotMachine.Common.Interfaces.Services
{
    public interface ICalculateProfitService
    {
        decimal Calculate(IList<IList<ISymbol>> symbolsTable, decimal stake);
    }
}
