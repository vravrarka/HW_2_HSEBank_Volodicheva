using Bank.MainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.App.Export
{
    public class CsvDataVisitor : DataVisitor
    {
        private readonly StringBuilder _accountsCsv = new StringBuilder();
        private readonly StringBuilder _categoriesCsv = new StringBuilder();
        private readonly StringBuilder _operationsCsv = new StringBuilder();
        public CsvDataVisitor()
        {
            _accountsCsv.AppendLine("Id,Name,Balance");
            _categoriesCsv.AppendLine("Id,Type,Name");
            _operationsCsv.AppendLine("Id,Type,BankAccountId,Amount,Date,CategoryId,Description");
        }
        public override void Visit(BankAccount account)
        {
            _accountsCsv.AppendLine($"{account.Id},{EscapeCsv(account.Name)},{account.Balance}");
        }
        public override void Visit(Category category)
        {
            _categoriesCsv.AppendLine($"{category.Id},{category.Type},{EscapeCsv(category.Name)}");
        }
        public override void Visit(Operation operation)
        {
            _operationsCsv.AppendLine($"{operation.Id},{operation.Type},{operation.BankAccountId}," +
                                     $"{operation.Amount},{operation.Date:yyyy-MM-dd},{operation.CategoryId}," +
                                     $"{EscapeCsv(operation.Description ?? "")}");
        }
        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value)) return "\"\"";
            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
                return $"\"{value.Replace("\"", "\"\"")}\"";
            return value;
        }
        public void SaveToFiles(string basePath)
        {
            File.WriteAllText($"{basePath}_accounts.csv", _accountsCsv.ToString(), Encoding.UTF8);
            File.WriteAllText($"{basePath}_categories.csv", _categoriesCsv.ToString(), Encoding.UTF8);
            File.WriteAllText($"{basePath}_operations.csv", _operationsCsv.ToString(), Encoding.UTF8);
        }
    }
}
