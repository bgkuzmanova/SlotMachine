using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace SlotMachine.DomainLogic.Services
{
    public class CalculateProfitService : ICalculateProfitService
    {
        public decimal Calculate(IList<IList<ISymbol>> symbolsTable, decimal stake)
        {
            decimal coefSum = 0;
            SymbolComparer comparer = new SymbolComparer();
            foreach (var symbolRow in symbolsTable)
            {
                if (symbolRow.Distinct(comparer).Count() == 1)
                {
                    foreach (var symbol in symbolRow)
                    {
                        coefSum += symbol.Coefficient;
                    }
                }
            }

            return coefSum * stake;
        }

        private class SymbolComparer : IEqualityComparer<ISymbol>
        {
            public bool Equals(ISymbol? x, ISymbol? y)
            {
                return x?.SymbolChar == y?.SymbolChar
                    || x?.SymbolChar == '*'
                    || y?.SymbolChar == '*';
            }

            public int GetHashCode([DisallowNull] ISymbol obj)
            {
                return obj == null ? 0 : obj.SymbolChar;
            }
        }
    }
}
