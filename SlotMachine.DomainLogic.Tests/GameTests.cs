using Moq;
using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Views;
using SlotMachine.Data.Models;

namespace SlotMachine.DomainLogic.Tests
{
    public class GameTests
    {
        private readonly Game game;
        private readonly IList<ISymbol> symbols;
        private readonly Mock<IScreenSettings> screenSettingsMock;
        private readonly Mock<IViewManager> viewManagerMock;

        public GameTests()
        {
            symbols = new List<ISymbol>();
            symbols.Add(new Symbol() { SymbolChar = 'A', FullName = "Apple", Coefficient = 0.5M, Probability = 50 });
            symbols.Add(new Symbol() { SymbolChar = 'B', FullName = "Banana", Coefficient = 0.3M, Probability = 40 });
            symbols.Add(new Symbol() { SymbolChar = 'C', FullName = "Citrus", Coefficient = 0.2M, Probability = 10 });

            screenSettingsMock = new Mock<IScreenSettings>();
            viewManagerMock = new Mock<IViewManager>();
            game = new Game(symbols, screenSettingsMock.Object);
        }

        [Fact]
        public void PlayGame_EnterCorrectDeposit_DepositSet()
        {
            //Arrange
            screenSettingsMock.Setup(s => s.Columns).Returns(3);
            screenSettingsMock.Setup(s => s.Rows).Returns(4);
            screenSettingsMock.Setup(s => s.InputTrials).Returns(2);

            viewManagerMock.Setup(vm => vm.ViewInput()).Returns("100");

            decimal expectedResult = 100M;
            //Act
            game.EnterDeposit(viewManagerMock.Object);

            //Assert
            Assert.Equal(expectedResult, game.DepositAmount);
        }

        [Theory]
        [InlineData("-12")]
        [InlineData("asd")]
        public void PlayGame_EnterIncorrectDeposit_OutputValidation(string input)
        {
            //Arrange
            screenSettingsMock.Setup(s => s.Columns).Returns(3);
            screenSettingsMock.Setup(s => s.Rows).Returns(4);
            screenSettingsMock.Setup(s => s.InputTrials).Returns(1);

            viewManagerMock.Setup(vm => vm.ViewInput()).Returns(input);

            bool expectedResult = false;
            decimal expectedDepositAmount = 0;
            //Act
            bool result = game.EnterDeposit(viewManagerMock.Object);

            //Assert
            Assert.Equal(expectedResult, result);
            Assert.Equal(expectedDepositAmount, game.DepositAmount);
        }

        [Fact]
        public void Rotation_AllInputCorrect_GamePlayed()
        {
            //Arrange
            game.DepositAmount = 10;
            viewManagerMock.Setup(vm => vm.ViewInput()).Returns("10");
            screenSettingsMock.Setup(s => s.Rows).Returns(4);
            screenSettingsMock.Setup(s => s.Columns).Returns(3);
            symbols.Clear();
            symbols.Add(new Symbol() { SymbolChar = 'A', FullName = "Apple", Coefficient = 0, Probability = 100 });

            decimal expectedDepositAmount = 0;

            //Act
            game.Rotation(viewManagerMock.Object);

            //Assert
            Assert.Equal(expectedDepositAmount, game.DepositAmount);
        }

        [Theory]
        [InlineData("-12", 10)]
        [InlineData("asd", 10)]
        [InlineData("20", 10)]
        public void Rotation_StakeInputIncorrect_OutputValidation(string stake, decimal depositAmount)
        {
            //Arrange
            game.DepositAmount = depositAmount;
            viewManagerMock.Setup(vm => vm.ViewInput()).Returns(stake);
            screenSettingsMock.Setup(s => s.Rows).Returns(4);
            screenSettingsMock.Setup(s => s.Columns).Returns(3);
            screenSettingsMock.Setup(s => s.InputTrials).Returns(1);
            bool expectedResult = false;

            //Act
            bool result = game.Rotation(viewManagerMock.Object);

            //Assert
            Assert.Equal(expectedResult, result);
        }
    }
}
