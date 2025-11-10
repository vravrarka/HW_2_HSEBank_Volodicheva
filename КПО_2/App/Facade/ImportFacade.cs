using Bank.App.Import;
using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Facade
{
    public class ImportFacade
    {
        private readonly Dictionary<string, DataImporter> _importers;
        private readonly FinancialAccountingFacade _financialFacade;
        public ImportFacade(FinancialAccountingFacade financialFacade)
        {
            _financialFacade = financialFacade;
            _importers = new Dictionary<string, DataImporter>
            {
                { ".csv", new CsvDataImporter() },
                { ".json", new JsonDataImporter() },
                { ".yaml", new YamlDataImporter() },
                { ".yml", new YamlDataImporter() }
            };
        }
        public void ImportBankAccounts(string filePath)
        {
            var importer = GetImporter(filePath);
            if (importer == null) return;
            var accounts = importer.ImportBankAccounts(filePath);
            foreach (var account in accounts)
            {
                _financialFacade.CreateAccount(account.Name, account.Balance);
            }
        }
        public void ImportCategories(string filePath)
        {
            var importer = GetImporter(filePath);
            if (importer == null) return;
            var categories = importer.ImportCategories(filePath);
            foreach (var category in categories)
            {
                _financialFacade.CreateCategory(category.Type, category.Name);
            }
        }
        public void ImportOperations(string filePath)
        {
            var importer = GetImporter(filePath);
            if (importer == null) return;
            var operations = importer.ImportOperations(filePath);
            int importedCount = 0;
            int skippedCount = 0;
            foreach (var operation in operations)
            {
                try
                {
                    _financialFacade.CreateOperation(
                        operation.Type,
                        operation.BankAccountId,
                        operation.Amount,
                        operation.Date,
                        operation.CategoryId,
                        operation.Description
                    );
                    importedCount++;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка при импорте операции: {ex.Message}");
                    skippedCount++;
                }
            }
        }
        private DataImporter GetImporter(string filePath)
        {
            var extension = System.IO.Path.GetExtension(filePath).ToLower();

            if (_importers.ContainsKey(extension))
            {
                return _importers[extension];
            }
            Console.WriteLine($"Неизвестный формат файла: {extension}");
            return null;
        }
        public void DisplayAvailableFormats()
        {
            Console.WriteLine("Доступные форматы импорта:");
            foreach (var format in _importers.Keys)
            {
                Console.WriteLine($" - {format}");
            }
        }
    }
}
