using Bank.App.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Commands
{
    public class ImportDataCommand : Command
    {
        private readonly FinancialAccountingFacade _facade;
        private readonly string _filePath;
        private readonly string _dataType;
        private readonly ImportFacade _importFacade;
        public ImportDataCommand(FinancialAccountingFacade facade, ImportFacade importFacade, string filePath, string dataType)
        {
            _facade = facade;
            _importFacade = importFacade;
            _filePath = filePath;
            _dataType = dataType.ToLower();
        }
        public override void Execute()
        {
            switch (_dataType)
            {
                case "accounts":
                    _importFacade.ImportBankAccounts(_filePath);
                    break;
                case "categories":
                    _importFacade.ImportCategories(_filePath);
                    break;
                case "operations":
                    _importFacade.ImportOperations(_filePath);
                    break;
                default:
                    Console.WriteLine($"Неизвестный тип данных для импорта: {_dataType}");
                    break;
            }
        }
        public override void Undo() { }
    }
}
