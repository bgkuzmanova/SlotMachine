using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using SlotMachine.Data.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlotMachine.Data.Tests
{
    public class ApplicationSettingsServiceTests
    {
        [Fact]
        public void GetScreenSettings_ValidJsonConfigFile_Passed()
        {
            //Arrange
            var filename = "appSettings.json";
            IApplicationSettingsService sut = new ApplicationSettingsService();

            //Act
            var result = sut.GetScreenSettings(filename);

            //Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IScreenSettings>(result);
        }

        [Fact]
        public void GetSettings_WrongFileName_ExceptionThrown()
        {
            //Arrange
            var filename = "wrongPath.json";
            IApplicationSettingsService sut = new ApplicationSettingsService();

            //Act
            //Assert
            Assert.Throws<FileNotFoundException>(() => sut.GetScreenSettings(filename));
        }
    }
}
