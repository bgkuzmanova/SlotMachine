using Moq;
using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using SlotMachine.Data.Models;
using SlotMachine.DomainLogic.Services;

namespace SlotMachine.DomainLogic.Tests
{
    public class SymbolGeneratorServiceTests
    {
        [Theory]
        [InlineData(51, 'A')]
        [InlineData(70, 'A')]
        [InlineData(100, 'A')]
        public void GetRandomSymbol_GenerateApple_AppleGenerated(int probability, char expectedSymbol)
        {
            //Arrange
            IList<ISymbol> symbols = new List<ISymbol>();
            symbols.Add(new Symbol() { SymbolChar = 'A', FullName = "Apple", Coefficient = 0.5M, Probability = 50 });
            symbols.Add(new Symbol() { SymbolChar = 'B', FullName = "Banana", Coefficient = 0.3M, Probability = 40 });
            symbols.Add(new Symbol() { SymbolChar = 'C', FullName = "Citrus", Coefficient = 0.2M, Probability = 10 });

            ISymbolGeneratorService sut = new SymbolGeneratorService(symbols);

            Mock<Random> randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(It.IsAny<int>(),It.IsAny<int>())).Returns(probability);

            //Act
            ISymbol result = sut.GetRandomSymbol(randomMock.Object);

            //Assert
            Assert.NotNull(result);
            Assert.Equal(expectedSymbol, result.SymbolChar);
        }

        [Fact]
        public void GetRandomSymbol_EmptySymbols_ThrowException()
        {
            //Arrange
            IList<ISymbol> symbols = new List<ISymbol>();
            ISymbolGeneratorService sut = new SymbolGeneratorService(symbols);

            Mock<Random> randomMock = new Mock<Random>();
            randomMock.Setup(r => r.Next(It.IsAny<int>(), It.IsAny<int>())).Returns(10);

            //Act
            //Assert
            Assert.Throws<InvalidOperationException>(() => sut.GetRandomSymbol(randomMock.Object));
        }
    }
}
