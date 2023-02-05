using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using SlotMachine.Data.Models;

namespace SlotMachine.DomainLogic.Services
{
    public class SymbolGeneratorService : ISymbolGeneratorService
    {
        private IList<ISymbol> symbols;

        public SymbolGeneratorService(IList<ISymbol> symbols)
        {
            this.symbols = symbols;
            Random = GenerateRandomizer();
        }

        public int MaxRange
        {
            get;
            private set;
        }

        public Random Random
        {
            get;
            private set;
        }

        public ISymbol GetRandomSymbol(Random random)
        {
            int randomNumber = random.Next(1, MaxRange);
            List<ISymbol> orderedSymbols = symbols.OrderBy(s => s.Probability).ToList();

            int cumulative = 0;
            for (int i = 0; i < orderedSymbols.Count; i++)
            {
                cumulative += orderedSymbols[i].Probability;
                if (randomNumber <= cumulative)
                {
                    return orderedSymbols[i];
                }
            }

            return orderedSymbols.Last();
        }

        private Random GenerateRandomizer()
        {
            int maxValue = 0;
            foreach (var symbol in symbols)
            {
                maxValue += symbol.Probability;
            }
            MaxRange = maxValue;

            return new Random();
        }
    }
}
