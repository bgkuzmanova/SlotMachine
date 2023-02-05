using Newtonsoft.Json;
using SlotMachine.Data.Models;
using SlotMachine.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SlotMachine.Common.Interfaces.Models;

namespace SlotMachine.Data.Services
{
    public class SymbolsDataService : ISymbolsDataService
    {
        public IEnumerable<ISymbol> GetSymbols(string fileName)
        {
            if (!File.Exists(fileName))
            {
                throw new FileNotFoundException();
            }

            IEnumerable<Symbol> symbols = new List<Symbol>();

            using (StreamReader r = new StreamReader(fileName))
            {
                string json = r.ReadToEnd();
                symbols = JsonConvert.DeserializeObject<List<Symbol>>(json);
            }
            return symbols;
        }
    }
}
