using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using SlotMachine.Data.Models;
using SlotMachine.DomainLogic.Services;

namespace SlotMachine.DomainLogic.Tests
{
    public class CalculateProfitServiceTests
    {
        [Theory]
        [InlineData (10,9)]
        [InlineData(20, 18)]
        public void Calculate_SymbolsFilledOneRow_PositiveProfit(decimal stake, decimal profit)
        {
            //Arrange
            ICalculateProfitService sut = new CalculateProfitService();
            IList<IList<ISymbol>> symbols = new List<IList<ISymbol>>();
            IList<ISymbol> row1 = new List<ISymbol>();
            ISymbol symbol = new Symbol() { SymbolChar = 'A', Coefficient = 0.3M, FullName = "qwe", Probability = 10 };
            row1.Add(symbol);
            row1.Add(symbol);
            row1.Add(symbol);
            symbols.Add(row1);

            decimal expectedResult = profit;

            //Act
            decimal result = sut.Calculate(symbols, stake);

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
