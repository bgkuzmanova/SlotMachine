using SlotMachine.Common.Interfaces.Models;

namespace SlotMachine.Common.Interfaces.Services
{
    public interface ISymbolGeneratorService
    {
        Random Random { get; }
        ISymbol GetRandomSymbol(Random random);
    }
}
