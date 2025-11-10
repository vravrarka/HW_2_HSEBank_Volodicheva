using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank.App.Export;

namespace Bank.App.Facade
{
    public class ExportFacade
    {
        private readonly FinancialAccountingFacade _financialFacade;
        public ExportFacade(FinancialAccountingFacade financialFacade)
        {
            _financialFacade = financialFacade;
        }
        public void ExportToCsv(string basePath)
        {
            var visitor = new CsvDataVisitor();
            VisitAllData(visitor);
            visitor.SaveToFiles(basePath);
            Console.WriteLine($"Данные экспортированы в CSV файлы с базовым именем: {basePath}");
        }
        public void ExportToJson(string basePath)
        {
            var visitor = new JsonDataVisitor();
            VisitAllData(visitor);
            visitor.SaveToFiles(basePath);
            Console.WriteLine($"Данные экспортированы в JSON файлы с базовым именем: {basePath}");
        }
        public void ExportToYaml(string basePath)
        {
            var visitor = new YamlDataVisitor();
            VisitAllData(visitor);
            visitor.SaveToFiles(basePath);
            Console.WriteLine($"Данные экспортированы в YAML файлы с базовым именем: {basePath}");
        }
        private void VisitAllData(DataVisitor visitor)
        {
            var bankAccountFacade = new BankAccountFacade();
            var categoryFacade = new CategoryFacade();
            var operationFacade = new OperationFacade(bankAccountFacade);
            foreach (var account in bankAccountFacade.GetAllBankAccounts())
            {
                account.Accept(visitor);
            }
            foreach (var category in categoryFacade.GetAllCategories())
            {
                category.Accept(visitor);
            }
            foreach (var operation in operationFacade.GetAllOperations())
            {
                operation.Accept(visitor);
            }
        }
    }
}
