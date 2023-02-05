using SlotMachine.Common.Exceptions;
using SlotMachine.Common.Interfaces.Models;
using SlotMachine.Common.Interfaces.Services;
using SlotMachine.Common.Interfaces.Views;
using SlotMachine.Data.Models;
using SlotMachine.DomainLogic.Services;

namespace SlotMachine.DomainLogic
{
    public class Game
    {
        #region Declarations
        private ISymbolGeneratorService symbolGeneratorService;
        private ICalculateProfitService calculateProfitService;
        private decimal depositAmount;
        private IScreenSettings screenSettings;
        #endregion

        #region Init
        public Game(IList<ISymbol> symbols, IScreenSettings screenSettings)
        {
            symbolGeneratorService = new SymbolGeneratorService(symbols);
            calculateProfitService = new CalculateProfitService();
            this.screenSettings = screenSettings;
        }
        #endregion

        #region Properties
        public decimal DepositAmount
        {
            get
            {
                return depositAmount;
            }
            set
            {
                if (value < 0)
                {
                    throw new NegativeAmountException();
                }
                depositAmount = value;
            }
        }
        #endregion

        #region Methods
        public void PlayGame()
        {
            //EnterDeposit();
            //Rotation();
        }

        public bool EnterDeposit(IViewManager viewManager)
        {
            viewManager.ViewOutput("Please, deposit money you would like to play with:");
            int trialsCount = 0;

            while (trialsCount <= screenSettings.InputTrials)
            {
                if (decimal.TryParse(viewManager.ViewInput(), out decimal depAmount)
                    && depAmount > 0)
                {
                    DepositAmount = Math.Round(depAmount, 2);
                    return true;
                }
                else
                {
                    trialsCount++;
                    viewManager.ViewOutput("Please, enter a valid value for amount (number)");
                }
            }
            return false;
        }

        public bool Rotation(IViewManager viewManager)
        {
            while (DepositAmount > 0)
            {
                decimal stake = EnterStake(viewManager);
                if (stake == 0)
                {
                    return false;
                }
                IList<IList<ISymbol>> screen = Rotate(viewManager);
                decimal profit = Math.Round(calculateProfitService.Calculate(screen, stake), 2);
                DepositAmount = AdjustDeposit(DepositAmount, stake, profit);
                viewManager.ViewOutput($"You have won {profit}");
                viewManager.ViewOutput($"Current balance is {DepositAmount}");
            }
            return true;
        }

        private decimal EnterStake(IViewManager viewManager)
        {
            viewManager.ViewOutput("Enter stake amount:");
            decimal stakeAmount = 0;
            int trialsCount = 0;

            while (trialsCount <= screenSettings.InputTrials)
            {
                if (decimal.TryParse(viewManager.ViewInput(), out decimal amount)
                    && amount > 0)
                {
                    if (amount > DepositAmount)
                    {
                        trialsCount++;
                        viewManager.ViewOutput("The stake can not be bigger than the deposit amount!");
                        viewManager.ViewOutput("Please, enter a new value:");
                        continue;
                    }
                    stakeAmount = Math.Round(amount, 2);
                    break;
                }
                else
                {
                    trialsCount++;
                    viewManager.ViewOutput("Please, enter a valid value for amount (number)");
                }
            }

            return stakeAmount;
        }

        private IList<IList<ISymbol>> Rotate(IViewManager viewManager)
        {
            IList<IList<ISymbol>> rotationResult = new List<IList<ISymbol>>();

            for (int i = 0; i < screenSettings.Rows; i++)
            {
                List<ISymbol> row = new List<ISymbol>();
                for (int j = 0; j < screenSettings.Columns; j++)
                {
                    ISymbol symbol = symbolGeneratorService.GetRandomSymbol(symbolGeneratorService.Random);
                    row.Add(symbol);
                    viewManager.ViewOutput(symbol.SymbolChar);
                }
                rotationResult.Add(row);
                viewManager.ViewOutput(string.Empty);
            }

            return rotationResult;
        }

        private decimal AdjustDeposit(decimal deposit, decimal stake, decimal profit)
        {
            return deposit - stake + profit;
        }
        #endregion
    }
}
