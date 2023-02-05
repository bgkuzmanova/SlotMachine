using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using SlotMachine.Data.Models;
using SlotMachine.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Data.Tests
{
    public class SymbolsDataServiceTests
    {
        [Fact]
        public void GetSymbols_ValidJsonWithSymbol_Passed()
        {
            //Arrange
            var filename = "SymbolsData.json";
            ISymbolsDataService sut = new SymbolsDataService();

            //Act
            var result = sut.GetSymbols(filename);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<ISymbol>>(result);
            Assert.IsAssignableFrom<ISymbol>(result.First());
        }

        [Fact]
        public void GetSymbols_WrongFileName_ExceptionThrown()
        {
            //Arrange
            var filename = "wrongPath.json";
            ISymbolsDataService sut = new SymbolsDataService();

            //Act
            //Assert
            Assert.Throws<FileNotFoundException>(() => sut.GetSymbols(filename));
        }
    }
}
