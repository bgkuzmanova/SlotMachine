using SlotMachine.Common.Exceptions;
using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using SlotMachine.Common.Interfaces.Views;
using SlotMachine.Data.Models;
using SlotMachine.Data.Services;
using SlotMachine.DomainLogic;
using SlotMachine.Views;
using System.ComponentModel;

class Program
{
    public static int COLUMNS = 3;
    public static int ROWS = 4;

    static void Main(string[] args)
    {
        IViewManager viewManager = new ConsoleIO();
        try
        {
            Game game = new Game(GetSymbols(), GetScreenSettings());
            if (game.EnterDeposit(viewManager))
            {
                _ = game.Rotation(viewManager);
            }
            viewManager.ViewOutput("Game over! Thank you for playing :)");
        }
        catch (NegativeAmountException)
        {
            viewManager.ViewOutput("Something went wrong! Value can not be negative!");
        }
        catch (FileNotFoundException)
        {
            viewManager.ViewOutput("Something went wrong! Initial settings file not found!");
        }
        catch
        {
            viewManager.ViewOutput("Something went terribly wrong :(");
        }
    }

    static IList<ISymbol> GetSymbols()
    {
        List<ISymbol> symbols = new List<ISymbol>();

        ISymbolsDataService symbolsDataService = new SymbolsDataService();
        symbols = symbolsDataService.GetSymbols("SymbolsData.json").ToList();
        return symbols;
    }
    static IScreenSettings GetScreenSettings()
    {
        IScreenSettings screenSettings = new ScreenSettings();
        IApplicationSettingsService applicationSettingsService = new ApplicationSettingsService();
        screenSettings = applicationSettingsService.GetScreenSettings("appSettings.json");
        return screenSettings;
    }
}